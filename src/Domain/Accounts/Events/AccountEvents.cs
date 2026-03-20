using TES.Domain.Accounts.Enums;
using Domain.Common;

namespace Domain.Accounts.Events;

/// <summary>
/// Disparado quando uma nova conta é criada.
/// Handlers: área de Fraude pode iniciar análise de risco.
/// </summary>
public record AccountCreatedEvent(Guid AccountId, string NomeTitular, string Cpf) : DomainEvent;

/// <summary>
/// Disparado quando dados da conta são alterados.
/// Handlers: área de Cartões pode atualizar cadastro vinculado.
/// </summary>
public record AccountUpdatedEvent(
    Guid AccountId,
    string NomeTitular,
    string Cpf,
    AccountStatus Status) : DomainEvent;

/// <summary>
/// Disparado quando uma conta é inativada/removida.
/// Handlers: área de Fraude pode encerrar monitoramento.
/// </summary>
public record AccountDeletedEvent(Guid AccountId) : DomainEvent;
