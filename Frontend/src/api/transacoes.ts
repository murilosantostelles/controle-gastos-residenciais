import { api } from "./client";
import type { Transacao, TipoTransacao } from "../types/Transacao";

export function listarTransacoes(): Promise<Transacao[]> {
  return api.get<Transacao[]>("/transacao");
}

export function criarTransacao(
  descricao: string,
  valor: number,
  tipo: TipoTransacao,
  pessoaId: number
): Promise<Transacao> {
  return api.post<Transacao>("/transacao", { descricao, valor, tipo, pessoaId });
}