using TES.Domain.Common;

namespace TES.Domain.Accounts.Events;

public sealed record AccountDeletedEvent(Guid AccountId) : IDomainEvent;
