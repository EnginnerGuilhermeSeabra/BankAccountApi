using MediatR;

namespace Domain.Common;

/// <summary>
/// Contrato base para todos os Domain Events.
/// Implementa INotification do MediatR para permitir dispatch via Mediator.
/// </summary>
public abstract record DomainEvent : INotification
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}
