using FluentValidation;

namespace TES.Application.Accounts.Commands.CreateAccount;

public sealed class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(x => x.NomeTitular)
            .NotEmpty().WithMessage("Nome do titular é obrigatório.")
            .MaximumLength(150);

        RuleFor(x => x.Cpf)
            .NotEmpty().WithMessage("CPF é obrigatório.")
            .Matches(@"^\d{11}$|^\d{3}\.\d{3}\.\d{3}-\d{2}$")
            .WithMessage("Formato de CPF inválido.");
    }
}
