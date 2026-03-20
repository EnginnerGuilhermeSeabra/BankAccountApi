using Microsoft.Extensions.Caching.Memory;
using TES.Application.Accounts.DTOs;
using TES.Application.Common.Interfaces;

namespace TES.Infrastructure.Cache;

/// <summary>
/// Implementa o padrão Cache-Aside com IMemoryCache.
/// TTL é calculado até o final do dia corrente UTC,
/// garantindo que dados consultados naquele dia não gerem IOPS adicionais.
/// </summary>
public sealed class AccountCacheService : IAccountCacheService
{
    private readonly IMemoryCache _cache;
    private const string Prefix = "account:";

    public AccountCacheService(IMemoryCache cache) => _cache = cache;

    private static string Key(Guid id) => $"{Prefix}{id}";

    private static TimeSpan TtlUntilEndOfDay()
    {
        var now = DateTime.UtcNow;
        var endOfDay = now.Date.AddDays(1);
        return endOfDay - now;
    }

    public AccountDto? Get(Guid id) =>
        _cache.TryGetValue(Key(id), out AccountDto? dto) ? dto : null;

    public void Set(Guid id, AccountDto dto)
    {
        var options = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TtlUntilEndOfDay())
            .SetSlidingExpiration(TimeSpan.FromMinutes(30));

        _cache.Set(Key(id), dto, options);
    }

    public void Invalidate(Guid id) => _cache.Remove(Key(id));
}
