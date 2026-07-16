import { api } from "./client";
import type { TotaisResultado } from "../types/Totais";

export function obterTotais(): Promise<TotaisResultado> {
  return api.get<TotaisResultado>("/totais");
}