using MediatR;
using TES.Application.Accounts.DTOs;

namespace TES.Application.Accounts.Queries.GetAccountById;

public sealed record GetAccountByIdQuery(Guid Id) : IRequest<AccountDto>;
