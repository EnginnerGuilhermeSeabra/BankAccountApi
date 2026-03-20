using MediatR;
using Microsoft.Extensions.Logging;
using TES.Domain.Accounts.Events;

namespace TES.Application.Accounts.EventHandlers;

/// <summary>
/// Simula a notificação ao módulo de Fraude quando uma conta é criada.
/// Em produção, publicaria uma mensagem em um broker (SQS, SNS, RabbitMQ).
/// </summary>
public sealed class AccountCreatedEventHandler : INotificationHandler<AccountCreatedEvent>
{
    private readonly ILogger<AccountCreatedEventHandler> _logger;

    public AccountCreatedEventHandler(ILogger<AccountCreatedEventHandler> logger)
        => _logger = logger;

    public Task Handle(AccountCreatedEvent notification, CancellationToken ct)
    {
        _logger.LogInformation(
            "[Fraude] Nova conta criada | AccountId: {AccountId} | Titular: {Titular}",
            notification.AccountId, notification.NomeTitular);

        // TODO: Publicar em fila SQS / SNS para análise de fraude
        return Task.CompletedTask;
    }
}
