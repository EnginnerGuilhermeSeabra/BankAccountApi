using TES.Domain.Common;

namespace TES.Domain.Accounts.Events;

public sealed record AccountCreatedEvent(
    Guid AccountId,
    string NomeTitular,
    string Cpf) : IDomainEvent;
