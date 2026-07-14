const BASE_URL = "http://localhost:5123/api";

/**
 * erro customizado para representar falhas vindas da api.
 */
export class ApiError extends Error {
  status: number;

  constructor(message: string, status: number) {
    super(message);
    this.status = status;
  }
}

/**
 * monta a url completa, trata erros
 * http de forma centralizada e já devolve o json parseado.
 */
async function request<T>(path: string, options?: RequestInit): Promise<T> {
  const response = await fetch(`${BASE_URL}${path}`, {
    headers: { "Content-Type": "application/json" },
    ...options,
  });

  if (!response.ok) {
    const problema = await response.json().catch(() => null);
    const mensagem = problema?.title ?? "Erro ao comunicar com o servidor.";
    throw new ApiError(mensagem, response.status);
  }

  if (response.status === 204) {
    return undefined as T;
  }

  return response.json();
}

export const api = {
  get: <T>(path: string) => request<T>(path),

  post: <T>(path: string, body: unknown) =>
    request<T>(path, {
      method: "POST",
      body: JSON.stringify(body),
    }),

  delete: (path: string) =>
    request<void>(path, { method: "DELETE" }),
};