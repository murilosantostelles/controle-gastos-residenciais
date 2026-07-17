import { useEffect, useState } from "react";
import "./TransacoesPage.css";
import type { Transacao, TipoTransacao } from "../types/Transacao";
import type { Pessoa } from "../types/Pessoa";
import { listarTransacoes, criarTransacao } from "../api/transacoes";
import { listarPessoas } from "../api/pessoas";
import { ApiError } from "../api/client";

export function TransacoesPage() {
  const [transacoes, setTransacoes] = useState<Transacao[]>([]);
  const [pessoas, setPessoas] = useState<Pessoa[]>([]);
  const [descricao, setDescricao] = useState("");
  const [valor, setValor] = useState("");
  const [tipo, setTipo] = useState<TipoTransacao>("Despesa");
  const [pessoaId, setPessoaId] = useState("");
  const [erro, setErro] = useState<string | null>(null);
  const [carregando, setCarregando] = useState(true);

  useEffect(() => {
    carregarDados();
  }, []);

  async function carregarDados() {
    try {
      setCarregando(true);
      const [dadosTransacoes, dadosPessoas] = await Promise.all([
        listarTransacoes(),
        listarPessoas(),
      ]);
      setTransacoes(dadosTransacoes);
      setPessoas(dadosPessoas);
    } catch {
      setErro("Não foi possível carregar os dados.");
    } finally {
      setCarregando(false);
    }
  }

  async function handleCriar(event: React.FormEvent) {
    event.preventDefault();
    setErro(null);

    try {
      await criarTransacao(descricao, Number(valor), tipo, Number(pessoaId));
      setDescricao("");
      setValor("");
      setPessoaId("");
      await carregarDados();
    } catch (e) {
      if (e instanceof ApiError) {
        setErro(e.message);
      } else {
        setErro("Erro inesperado ao criar transação.");
      }
    }
  }

  function nomeDaPessoa(id: number): string {
    return pessoas.find((p) => p.id === id)?.nome ?? "Desconhecida";
  }

  return (
    <div className="transacoes-page">
      <h2>Transações</h2>

      <form className="transacoes-form" onSubmit={handleCriar}>
        <input
          type="text"
          placeholder="Descrição"
          value={descricao}
          onChange={(e) => setDescricao(e.target.value)}
          required
        />
        <input
          type="number"
          step="0.01"
          placeholder="Valor"
          value={valor}
          onChange={(e) => setValor(e.target.value)}
          required
        />
        <select value={tipo} onChange={(e) => setTipo(e.target.value as TipoTransacao)}>
          <option value="Despesa">Despesa</option>
          <option value="Receita">Receita</option>
        </select>
        <select value={pessoaId} onChange={(e) => setPessoaId(e.target.value)} required>
          <option value="">Selecione a pessoa</option>
          {pessoas.map((pessoa) => (
            <option key={pessoa.id} value={pessoa.id}>
              {pessoa.nome}
            </option>
          ))}
        </select>
        <button type="submit">Adicionar</button>
      </form>

      {erro && <p className="transacoes-erro">{erro}</p>}

      {carregando ? (
        <p>Carregando...</p>
      ) : (
        <ul className="transacoes-lista">
          {transacoes.map((transacao) => (
            <li key={transacao.id} className="transacoes-item">
              <span>
                <strong>{transacao.descricao}</strong> — {nomeDaPessoa(transacao.pessoaId)}
              </span>
              <span className={transacao.tipo === "Receita" ? "valor-receita" : "valor-despesa"}>
                {transacao.tipo === "Receita" ? "+" : "-"} R$ {transacao.valor.toFixed(2)}
              </span>
            </li>
          ))}
        </ul>
      )}
    </div>
  );
}