using MediatR;
using Microsoft.Extensions.Logging;
using TES.Domain.Accounts.Events;

namespace TES.Application.Accounts.EventHandlers;

/// <summary>
/// Notifica o módulo de Cartões sobre alterações na conta.
/// </summary>
public sealed class AccountUpdatedEventHandler : INotificationHandler<AccountUpdatedEvent>
{
    private readonly ILogger<AccountUpdatedEventHandler> _logger;

    public AccountUpdatedEventHandler(ILogger<AccountUpdatedEventHandler> logger)
        => _logger = logger;

    public Task Handle(AccountUpdatedEvent notification, CancellationToken ct)
    {
        _logger.LogInformation(
            "[Cartões] Conta atualizada | AccountId: {AccountId} | Status: {Status}",
            notification.AccountId, notification.Status);

        return Task.CompletedTask;
    }
}
