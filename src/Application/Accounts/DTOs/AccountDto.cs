using TES.Domain.Accounts.Enums;

namespace TES.Application.Accounts.DTOs;

public sealed record AccountDto(
    Guid Id,
    string NomeTitular,
    string Cpf,
    AccountStatus Status,
    AccountEvent LastEvent,
    DateTime CriadoEm,
    DateTime? AtualizadoEm);
