using MediatR;
using TES.Application.Accounts.DTOs;

namespace TES.Application.Accounts.Queries.GetAllAccounts;

public sealed record GetAllAccountsQuery : IRequest<IReadOnlyList<AccountDto>>;
