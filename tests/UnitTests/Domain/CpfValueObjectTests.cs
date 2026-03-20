using FluentAssertions;
using TES.Domain.Accounts.ValueObjects;
using TES.Domain.Common;
using Xunit;

namespace TES.UnitTests.Domain;

public sealed class CpfValueObjectTests
{
    [Theory]
    [InlineData("529.982.247-25")]
    [InlineData("52998224725")]
    public void Create_DeveAceitar_CpfValido(string cpf)
    {
        var result = Cpf.Create(cpf);
        result.Value.Should().Be("52998224725");
    }

    [Theory]
    [InlineData("00000000000")]
    [InlineData("11111111111")]
    [InlineData("123")]
    [InlineData("")]
    public void Create_DeveLancar_QuandoCpfInvalido(string cpf)
    {
        var act = () => Cpf.Create(cpf);
        act.Should().Throw<DomainException>();
    }

    [Fact]
    public void ToString_DeveFormatarComMascaraCorreta()
    {
        var cpf = Cpf.Create("52998224725");
        cpf.ToString().Should().Be("529.982.247-25");
    }
}
