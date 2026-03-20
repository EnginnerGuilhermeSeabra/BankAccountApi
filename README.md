# TES – Sistema de Gestão de Contas Bancárias (Fintech API)

API RESTful construída com **.NET 8**, **DDD**, **CQRS/MediatR** e **EF Core In-Memory**.

## Arquitetura

```
BankAccountApi/
├── src/
│   ├── Domain/           # Entidades, Value Objects, Eventos, Interfaces
│   ├── Application/      # Commands, Queries, Handlers, DTOs, EventHandlers
│   ├── Infrastructure/   # EF Core, Repositórios, Cache (IMemoryCache)
│   └── WebAPI/           # Controllers MVC, Middleware, Program.cs
└── tests/
    └── UnitTests/        # xUnit + Moq + FluentAssertions
```

## Como executar

```bash
cd src/WebAPI
dotnet run
# Swagger UI: https://localhost:5001/swagger
```

## Executar testes

```bash
cd tests/UnitTests
dotnet test
```

## Decisões técnicas

| Preocupação           | Solução                                               |
|-----------------------|-------------------------------------------------------|
| Desacoplamento        | MediatR – Domain Events notificam Fraude e Cartões    |
| Custo de IOPS (AWS)   | Cache-Aside com IMemoryCache; TTL = fim do dia UTC    |
| Integridade do CPF    | Value Object com validação – sem lógica espalhada     |
| Soft Delete           | `Account.Inativar()` → status = Inativa + Domain Event|
| Tratamento de erros   | ExceptionMiddleware global → JSON padronizado         |
