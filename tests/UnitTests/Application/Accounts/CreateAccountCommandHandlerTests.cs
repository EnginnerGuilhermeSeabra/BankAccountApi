using FluentAssertions;
using MediatR;
using Moq;
using TES.Application.Accounts.Commands.CreateAccount;
using TES.Domain.Accounts.Entities;
using TES.Domain.Accounts.Repositories;
using TES.Domain.Common;
using Xunit;

namespace TES.UnitTests.Application.Accounts;

public sealed class CreateAccountCommandHandlerTests
{
    private readonly Mock<IAccountRepository> _repoMock = new();
    private readonly Mock<IPublisher> _publisherMock = new();

    private CreateAccountCommandHandler CreateHandler() =>
        new(_repoMock.Object, _publisherMock.Object);

    [Fact]
    public async Task Handle_DeveRetornarAccountDto_QuandoDadosSaoValidos()
    {
        // Arrange
        var command = new CreateAccountCommand("João Silva", "52998224725");

        _repoMock.Setup(r => r.ExistsByCpfAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                 .ReturnsAsync(false);
        _repoMock.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
                 .ReturnsAsync(1);

        var handler = CreateHandler();

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.NomeTitular.Should().Be("João Silva");
        result.Id.Should().NotBeEmpty();

        _repoMock.Verify(r => r.AddAsync(It.IsAny<Account>(), It.IsAny<CancellationToken>()), Times.Once);
        _repoMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        _publisherMock.Verify(p => p.Publish(It.IsAny<INotification>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_DeveLancarDomainException_QuandoCpfJaExiste()
    {
        // Arrange
        var command = new CreateAccountCommand("Maria Souza", "52998224725");

        _repoMock.Setup(r => r.ExistsByCpfAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                 .ReturnsAsync(true);

        var handler = CreateHandler();

        // Act
        var act = () => handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<DomainException>()
            .WithMessage("*CPF*");
    }

    [Fact]
    public async Task Handle_DeveLancarDomainException_QuandoCpfEhInvalido()
    {
        // Arrange
        var command = new CreateAccountCommand("Carlos Lima", "00000000000");

        _repoMock.Setup(r => r.ExistsByCpfAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                 .ReturnsAsync(false);

        var handler = CreateHandler();

        // Act
        var act = () => handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<DomainException>()
            .WithMessage("*inválido*");
    }
}
