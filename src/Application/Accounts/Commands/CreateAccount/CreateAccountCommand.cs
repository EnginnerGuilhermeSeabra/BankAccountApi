using MediatR;
using TES.Application.Accounts.DTOs;

namespace TES.Application.Accounts.Commands.CreateAccount;

public sealed record CreateAccountCommand(
    string NomeTitular,
    string Cpf) : IRequest<AccountDto>;
