using Bogus;
using Bogus.Extensions.Brazil;
using FoodChallenge.Common.Interfaces;
using FoodChallenge.Common.Validators;
using FoodChallenge.Order.Application.Clientes;
using FoodChallenge.Order.Application.Clientes.UseCases;
using FoodChallenge.Order.Domain.Clientes;
using FoodChallenge.Order.Domain.Clientes.ValueObjects;
using FoodChallenge.Order.Domain.Globalization;
using Moq;

namespace FoodChallenge.Order.UnitTests.Clientes.UseCases;

public class RegistraClienteUseCaseTests : TestBase
{
    private readonly Faker _faker;
    private readonly ValidationContext _validationContext;
    private readonly Mock<IClienteGateway> _gateway;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly RegistraClienteUseCase _useCase;

    public RegistraClienteUseCaseTests()
    {
        _faker = new Faker();
        _faker = GetFaker();
        _validationContext = new ValidationContext();
        _gateway = new Mock<IClienteGateway>();
        _unitOfWork = new Mock<IUnitOfWork>();

        _useCase = new RegistraClienteUseCase(_validationContext, _unitOfWork.Object, _gateway.Object);
    }

    [Fact]
    public async Task ExecutarAsync_DeveCadastrarCliente_QuandoOcorrerSucesso()
    {
        // Arrange
        _unitOfWork.Setup(u => u.BeginTransaction());
        _unitOfWork.Setup(u => u.CommitAsync()).Returns(Task.CompletedTask);

        var cliente = new Cliente(_faker.Person.Cpf());
        var clienteCadastrado = new Cliente(_faker.Person.Cpf());

        _gateway
            .Setup(g => g.ObterPorCpfAsync(cliente.Cpf, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Cliente)null);

        _gateway
            .Setup(g => g.CadastrarClienteAsync(cliente, It.IsAny<CancellationToken>()))
            .ReturnsAsync(clienteCadastrado);

        // Act
        var result = await _useCase.ExecutarAsync(cliente, CancellationToken.None);

        // Assert
        Assert.Same(clienteCadastrado, result);

        _gateway.Verify(g => g.ObterPorCpfAsync(cliente.Cpf, It.IsAny<CancellationToken>()), Times.Once);
        _gateway.Verify(g => g.CadastrarClienteAsync(cliente, It.IsAny<CancellationToken>()), Times.Once);

        _unitOfWork.Verify(u => u.BeginTransaction(), Times.Once);
        _unitOfWork.Verify(u => u.CommitAsync(), Times.Once);
        _unitOfWork.Verify(u => u.RollbackAsync(), Times.Never);
    }

    [Fact]
    public async Task ExecutarAsync_DeveRetornarValidacao_QuandoCpfJaCadastrado()
    {
        // Arrange
        var cliente = new Cliente(_faker.Person.Cpf());
        var validationMessages = new List<string>
        {
            string.Format(Textos.RegistroJaCadastrado, nameof(Cliente), nameof(Cliente.Cpf))
        };

        _gateway
            .Setup(g => g.ObterPorCpfAsync(cliente.Cpf, It.IsAny<CancellationToken>()))
            .ReturnsAsync(cliente);

        // Act
        var result = await _useCase.ExecutarAsync(cliente, CancellationToken.None);

        // Assert
        Assert.Null(result);
        Assert.Equal(validationMessages, _validationContext.ValidationMessages);
        Assert.True(_validationContext.HasValidations);

        _gateway.Verify(g => g.ObterPorCpfAsync(cliente.Cpf, It.IsAny<CancellationToken>()), Times.Once);

        _gateway.Verify(g => g.CadastrarClienteAsync(It.IsAny<Cliente>(), It.IsAny<CancellationToken>()), Times.Never);
        _unitOfWork.Verify(u => u.BeginTransaction(), Times.Never);
        _unitOfWork.Verify(u => u.CommitAsync(), Times.Never);
        _unitOfWork.Verify(u => u.RollbackAsync(), Times.Never);
    }

    [Fact]
    public async Task ExecutarAsync_EmCasoDeExcecao_DeveFazerRollback_ERethrow()
    {
        // Arrange
        _unitOfWork.Setup(u => u.BeginTransaction());
        _unitOfWork.Setup(u => u.RollbackAsync()).Returns(Task.CompletedTask);

        var cliente = new Cliente(_faker.Person.Cpf())
        {
            Nome = _faker.Person.FullName,
            Email = new Email(_faker.Person.Email)
        };
        var exceptionExpected = new InvalidOperationException("Falha ao cadastrar");

        _gateway
            .Setup(g => g.ObterPorCpfAsync(cliente.Cpf, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Cliente)null);

        _gateway
            .Setup(g => g.CadastrarClienteAsync(cliente, It.IsAny<CancellationToken>()))
            .ThrowsAsync(exceptionExpected);

        // Act
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _useCase.ExecutarAsync(cliente, CancellationToken.None));

        // Assert
        Assert.Same(exceptionExpected, exception);

        _gateway.Verify(g => g.ObterPorCpfAsync(cliente.Cpf, It.IsAny<CancellationToken>()), Times.Once);
        _gateway.Verify(g => g.CadastrarClienteAsync(cliente, It.IsAny<CancellationToken>()), Times.Once);

        _unitOfWork.Verify(u => u.BeginTransaction(), Times.Once);
        _unitOfWork.Verify(u => u.RollbackAsync(), Times.Once);
        _unitOfWork.Verify(u => u.CommitAsync(), Times.Never);
    }
}
