import { useState } from "react";
import "./App.css";
import { PessoasPage } from "./pages/PessoasPage";
import { TransacoesPage } from "./pages/TransacoesPage";
import { TotaisPage } from "./pages/TotaisPage";

type Aba = "pessoas" | "transacoes" | "totais";

export function App() {
  const [abaAtiva, setAbaAtiva] = useState<Aba>("pessoas");

  return (
    <div className="app-container">
      <h1 className="app-titulo">Controle de Gastos Residenciais</h1>

      <nav className="app-nav">
        <BotaoAba label="Pessoas" ativo={abaAtiva === "pessoas"} onClick={() => setAbaAtiva("pessoas")} />
        <BotaoAba label="Transações" ativo={abaAtiva === "transacoes"} onClick={() => setAbaAtiva("transacoes")} />
        <BotaoAba label="Totais" ativo={abaAtiva === "totais"} onClick={() => setAbaAtiva("totais")} />
      </nav>

      {abaAtiva === "pessoas" && <PessoasPage />}
      {abaAtiva === "transacoes" && <TransacoesPage />}
      {abaAtiva === "totais" && <TotaisPage />}
    </div>
  );
}

interface BotaoAbaProps {
  label: string;
  ativo: boolean;
  onClick: () => void;
}

function BotaoAba({ label, ativo, onClick }: BotaoAbaProps) {
  return (
    <button className={`botao-aba ${ativo ? "ativo" : ""}`} onClick={onClick}>
      {label}
    </button>
  );
}