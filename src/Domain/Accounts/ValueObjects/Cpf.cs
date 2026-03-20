using TES.Domain.Common;

namespace TES.Domain.Accounts.ValueObjects;

/// <summary>
/// Value Object que encapsula a lógica de validação do CPF.
/// Garante que nenhum CPF inválido seja persistido no domínio.
/// </summary>
public sealed class Cpf : IEquatable<Cpf>
{
    public string Value { get; }

    private Cpf(string value) => Value = value;

    public static Cpf Create(string cpf)
    {
        var digits = new string(cpf.Where(char.IsDigit).ToArray());

        if (!IsValid(digits))
            throw new DomainException($"CPF '{cpf}' é inválido.");

        return new Cpf(digits);
    }

    private static bool IsValid(string cpf)
    {
        if (cpf.Length != 11 || cpf.Distinct().Count() == 1) return false;

        static int CalcDigit(string s, int len)
        {
            int sum = s.Take(len).Select((c, i) => (c - '0') * (len + 1 - i)).Sum();
            int rem = sum % 11;
            return rem < 2 ? 0 : 11 - rem;
        }

        return cpf[9] - '0' == CalcDigit(cpf, 9)
            && cpf[10] - '0' == CalcDigit(cpf, 10);
    }

    public bool Equals(Cpf? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => Equals(obj as Cpf);
    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() =>
        $"{Value[..3]}.{Value[3..6]}.{Value[6..9]}-{Value[9..]}";
}
