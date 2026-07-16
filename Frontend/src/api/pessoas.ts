import { api } from "./client";
import type { Pessoa } from "../types/Pessoa";

export function listarPessoas(): Promise<Pessoa[]> {
  return api.get<Pessoa[]>("/pessoa");
}

export function criarPessoa(nome: string, idade: number): Promise<Pessoa> {
  return api.post<Pessoa>("/pessoa", { nome, idade });
}

export function removerPessoa(id: number): Promise<void> {
  return api.delete(`/pessoa/${id}`);
}