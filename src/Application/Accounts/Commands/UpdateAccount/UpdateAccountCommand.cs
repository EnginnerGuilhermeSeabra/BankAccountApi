using MediatR;
using TES.Application.Accounts.DTOs;
using TES.Domain.Accounts.Enums;

namespace TES.Application.Accounts.Commands.UpdateAccount;

public sealed record UpdateAccountCommand(
    Guid Id,
    string NomeTitular,
    AccountStatus Status) : IRequest<AccountDto>;
