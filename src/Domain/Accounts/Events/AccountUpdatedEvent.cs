using TES.Domain.Accounts.Enums;
using TES.Domain.Common;

namespace TES.Domain.Accounts.Events;

public sealed record AccountUpdatedEvent(
    Guid AccountId,
    string NomeTitular,
    AccountStatus Status) : IDomainEvent;
