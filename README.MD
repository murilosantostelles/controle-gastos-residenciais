# Controle de Gastos Residenciais

Sistema de controle de gastos residenciais com cadastro de pessoas, cadastro de transações e consulta de totais.

## Tecnologias

- **Back-end:** .NET 10 / C#, ASP.NET Core, Entity Framework Core, PostgreSQL
- **Front-end:** React + TypeScript (Vite)
- **Infraestrutura:** Docker (PostgreSQL)

## Arquitetura

O back-end segue uma arquitetura em camadas (Clean Architecture), com 4 projetos:

- **`ControleGastos.Domain`** — entidades do sistema (`Pessoa`, `Transacao`), sem nenhuma dependência externa.
- **`ControleGastos.Application`** — regras de negócio (Services) e contratos de acesso a dados (Interfaces).
- **`ControleGastos.Infrastructure`** — implementação do acesso a dados com Entity Framework Core e PostgreSQL.
- **`ControleGastos.Api`** — controllers REST, DTOs, tratamento de erros e configuração da aplicação.

A regra de dependência é sempre em direção ao `Domain`: `Api → Application/Infrastructure → Domain`. Isso garante que a lógica de negócio não depende de detalhes de banco de dados ou de framework web.

### Principais decisões técnicas

- **EF Core Migrations** para versionamento do schema do banco, em vez de criação automática de tabelas.
- **Cascade delete** configurado via Fluent API (`OnModelCreating`), garantindo que, ao deletar uma pessoa, todas as suas transações sejam removidas automaticamente pelo próprio banco de dados.
- **DTOs de entrada e saída** (`Request`/`Response`), evitando expor as entidades de domínio diretamente na API.
- **Tratamento de erros centralizado** via `IExceptionHandler`, padronizando as respostas de erro em formato `ProblemDetails` (RFC 9110).
- **Validação de entrada** via Data Annotations nos DTOs, com validação automática do ASP.NET Core.

## Regras de negócio implementadas

- Toda pessoa possui um identificador único gerado automaticamente.
- Ao deletar uma pessoa, todas as suas transações são removidas em cascata.
- Pessoas menores de 18 anos só podem cadastrar transações do tipo despesa.
- Toda transação precisa referenciar uma pessoa existente.
- A consulta de totais exibe, para cada pessoa, o total de receitas, despesas e o saldo; ao final, exibe o total geral.

## Como rodar o projeto

### Pré-requisitos

- [.NET SDK 10](https://dotnet.microsoft.com/download)
- [Node.js 20+](https://nodejs.org/)
- [Docker](https://www.docker.com/)

### 1. Subir o banco de dados

Na raiz do projeto:
```bash
docker compose up -d
```

### 2. Rodar as migrations

```bash
cd Backend/ControleGastos.Api
dotnet ef database update --project ../ControleGastos.Infrastructure --startup-project .
```

### 3. Rodar o back-end

```bash
cd Backend/ControleGastos.Api
dotnet run
```

A API estará disponível em `http://localhost:5123`.

### 4. Rodar o front-end

Em outro terminal:
```bash
cd Frontend
npm install
npm run dev
```

O frontend estará disponível em `http://localhost:5173`.

## Endpoints principais

| Método | Rota                | Descrição                          |
|--------|----------------------|-------------------------------------|
| GET    | `/api/pessoa`        | Lista todas as pessoas              |
| POST   | `/api/pessoa`        | Cadastra uma nova pessoa            |
| DELETE | `/api/pessoa/{id}`   | Remove uma pessoa e suas transações |
| GET    | `/api/transacao`     | Lista todas as transações           |
| POST   | `/api/transacao`     | Cadastra uma nova transação         |
| GET    | `/api/totais`        | Consulta os totais por pessoa e geral |