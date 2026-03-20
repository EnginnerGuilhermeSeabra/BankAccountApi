using TES.Application.Accounts.DTOs;

namespace TES.Application.Common.Interfaces;

/// <summary>
/// Abstração para o serviço de cache de contas.
/// O TTL é configurado para expirar ao final do dia corrente,
/// evitando consultas redundantes ao banco (reduz IOPS na AWS).
/// </summary>
public interface IAccountCacheService
{
    AccountDto? Get(Guid id);
    void Set(Guid id, AccountDto dto);
    void Invalidate(Guid id);
}
