using MediatR;
using Microsoft.Extensions.Logging;
using TES.Domain.Accounts.Events;

namespace TES.Application.Accounts.EventHandlers;

public sealed class AccountDeletedEventHandler : INotificationHandler<AccountDeletedEvent>
{
    private readonly ILogger<AccountDeletedEventHandler> _logger;

    public AccountDeletedEventHandler(ILogger<AccountDeletedEventHandler> logger)
        => _logger = logger;

    public Task Handle(AccountDeletedEvent notification, CancellationToken ct)
    {
        _logger.LogInformation(
            "[Fraude][Cartões] Conta inativada | AccountId: {AccountId}",
            notification.AccountId);

        return Task.CompletedTask;
    }
}
