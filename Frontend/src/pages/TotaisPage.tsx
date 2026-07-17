import { useEffect, useState } from "react";
import "./TotaisPage.css";
import type { TotaisResultado } from "../types/Totais";
import { obterTotais } from "../api/totais";

export function TotaisPage() {
  const [totais, setTotais] = useState<TotaisResultado | null>(null);
  const [erro, setErro] = useState<string | null>(null);
  const [carregando, setCarregando] = useState(true);

  useEffect(() => {
    carregarTotais();
  }, []);

  async function carregarTotais() {
    try {
      setCarregando(true);
      const dados = await obterTotais();
      setTotais(dados);
    } catch {
      setErro("Não foi possível carregar os totais.");
    } finally {
      setCarregando(false);
    }
  }

  function formatarMoeda(valor: number): string {
    return valor.toLocaleString("pt-BR", { style: "currency", currency: "BRL" });
  }

  if (carregando) return <p>Carregando...</p>;
  if (erro) return <p className="totais-erro">{erro}</p>;
  if (!totais) return null;

  return (
    <div className="totais-page">
      <h2>Totais</h2>

      <ul className="totais-lista">
        {totais.pessoas.map((pessoa) => (
          <li key={pessoa.pessoaId} className="totais-item">
            <strong>{pessoa.nome}</strong>
            <div className="totais-detalhe">
              <span className="valor-receita">Receitas: {formatarMoeda(pessoa.totalReceitas)}</span>
              <span className="valor-despesa">Despesas: {formatarMoeda(pessoa.totalDespesas)}</span>
              <span>Saldo: {formatarMoeda(pessoa.saldo)}</span>
            </div>
          </li>
        ))}
      </ul>

      <div className="totais-geral">
        <h3>Total geral</h3>
        <span className="valor-receita">Receitas: {formatarMoeda(totais.totalGeralReceitas)}</span>
        <span className="valor-despesa">Despesas: {formatarMoeda(totais.totalGeralDespesas)}</span>
        <span>Saldo: {formatarMoeda(totais.saldoGeral)}</span>
      </div>
    </div>
  );
}