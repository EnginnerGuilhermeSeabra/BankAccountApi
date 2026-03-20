using TES.Application.Accounts.DTOs;
using TES.Domain.Accounts.Entities;

namespace TES.Application.Accounts.DTOs;

public static class MappingExtensions
{
    public static AccountDto ToDto(this Account a) =>
        new(a.Id, a.NomeTitular, a.Cpf.ToString(), a.Status, a.LastEvent, a.CriadoEm, a.AtualizadoEm);
}
