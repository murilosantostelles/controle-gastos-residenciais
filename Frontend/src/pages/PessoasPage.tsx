import { useEffect, useState } from "react";
import "./PessoasPage.css";
import type { Pessoa } from "../types/Pessoa";
import { listarPessoas, criarPessoa, removerPessoa } from "../api/pessoas";
import { ApiError } from "../api/client";

export function PessoasPage() {
  const [pessoas, setPessoas] = useState<Pessoa[]>([]);
  const [nome, setNome] = useState("");
  const [idade, setIdade] = useState("");
  const [erro, setErro] = useState<string | null>(null);
  const [carregando, setCarregando] = useState(true);

  // Carrega a lista de pessoas assim que a página é exibida.
  useEffect(() => {
    carregarPessoas();
  }, []);

  async function carregarPessoas() {
    try {
      setCarregando(true);
      const dados = await listarPessoas();
      setPessoas(dados);
    } catch {
      setErro("Não foi possível carregar as pessoas.");
    } finally {
      setCarregando(false);
    }
  }

  async function handleCriar(event: React.FormEvent) {
    event.preventDefault();
    setErro(null);

    try {
      await criarPessoa(nome, Number(idade));
      setNome("");
      setIdade("");
      await carregarPessoas();
    } catch (e) {
      if (e instanceof ApiError) {
        setErro(e.message);
      } else {
        setErro("Erro inesperado ao criar pessoa.");
      }
    }
  }

  async function handleRemover(id: number) {
    setErro(null);

    try {
      await removerPessoa(id);
      await carregarPessoas();
    } catch (e) {
      if (e instanceof ApiError) {
        setErro(e.message);
      } else {
        setErro("Erro inesperado ao remover pessoa.");
      }
    }
  }

  return (
    <div className="pessoas-page">
      <h2>Pessoas</h2>

      <form className="pessoas-form" onSubmit={handleCriar}>
        <input
          type="text"
          placeholder="Nome"
          value={nome}
          onChange={(e) => setNome(e.target.value)}
          required
        />
        <input
          type="number"
          placeholder="Idade"
          value={idade}
          onChange={(e) => setIdade(e.target.value)}
          required
        />
        <button type="submit">Adicionar</button>
      </form>

      {erro && <p className="pessoas-erro">{erro}</p>}

      {carregando ? (
        <p>Carregando...</p>
      ) : (
        <ul className="pessoas-lista">
          {pessoas.map((pessoa) => (
            <li key={pessoa.id} className="pessoas-item">
              <span>
                {pessoa.nome} ({pessoa.idade} anos)
              </span>
              <button onClick={() => handleRemover(pessoa.id)}>Remover</button>
            </li>
          ))}
        </ul>
      )}
    </div>
  );
}