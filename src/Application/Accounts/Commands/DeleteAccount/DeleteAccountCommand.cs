using MediatR;

namespace TES.Application.Accounts.Commands.DeleteAccount;

public sealed record DeleteAccountCommand(Guid Id) : IRequest;
