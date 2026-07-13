export interface TotalPessoa {
  pessoaId: number;
  nome: string;
  totalReceitas: number;
  totalDespesas: number;
  saldo: number;
}

export interface TotaisResultado {
  pessoas: TotalPessoa[];
  totalGeralReceitas: number;
  totalGeralDespesas: number;
  saldoGeral: number;
}