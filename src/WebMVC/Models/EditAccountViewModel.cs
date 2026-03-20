using TES.Domain.Accounts.Enums;

namespace TES.WebMVC.Models;

public class EditAccountViewModel
{
    public Guid Id { get; set; }
    public string NomeTitular { get; set; } = string.Empty;
    public AccountStatus Status { get; set; }
}
