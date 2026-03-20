using MediatR;
using TES.Application.Accounts.DTOs;
using TES.Domain.Accounts.Repositories;

namespace TES.Application.Accounts.Queries.GetAllAccounts;

public sealed class GetAllAccountsQueryHandler
    : IRequestHandler<GetAllAccountsQuery, IReadOnlyList<AccountDto>>
{
    private readonly IAccountRepository _repo;

    public GetAllAccountsQueryHandler(IAccountRepository repo) => _repo = repo;

    public async Task<IReadOnlyList<AccountDto>> Handle(GetAllAccountsQuery request, CancellationToken ct)
    {
        var accounts = await _repo.GetAllAsync(ct);
        return accounts
            .Select(a => a.ToDto())
            .ToList();
    }
}
