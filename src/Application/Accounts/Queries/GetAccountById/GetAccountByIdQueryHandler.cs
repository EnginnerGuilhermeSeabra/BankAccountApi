using MediatR;
using TES.Application.Accounts.DTOs;
using TES.Application.Common.Interfaces;
using TES.Domain.Accounts.Entities;
using TES.Domain.Accounts.Repositories;
using TES.Domain.Common;

namespace TES.Application.Accounts.Queries.GetAccountById;

public sealed class GetAccountByIdQueryHandler
    : IRequestHandler<GetAccountByIdQuery, AccountDto>
{
    private readonly IAccountRepository _repo;
    private readonly IAccountCacheService _cache;

    public GetAccountByIdQueryHandler(IAccountRepository repo, IAccountCacheService cache)
    {
        _repo = repo;
        _cache = cache;
    }

    public async Task<AccountDto> Handle(GetAccountByIdQuery request, CancellationToken ct)
    {
        // Cache-Aside Pattern: verifica cache antes de ir ao banco
        var cached = _cache.Get(request.Id);
        if (cached is not null) return cached;

        var account = await _repo.GetByIdAsync(request.Id, ct)
            ?? throw new NotFoundException(nameof(Account), request.Id);

        var dto = account.ToDto();
        _cache.Set(request.Id, dto);

        return dto;
    }
}
