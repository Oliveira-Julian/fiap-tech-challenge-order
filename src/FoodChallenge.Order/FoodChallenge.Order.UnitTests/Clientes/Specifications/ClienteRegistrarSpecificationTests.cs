using Bogus;
using Bogus.Extensions.Brazil;
using FoodChallenge.Order.Application.Clientes;
using FoodChallenge.Order.Application.Clientes.Specifications;
using FoodChallenge.Order.Domain.Clientes;
using FoodChallenge.Order.Domain.Clientes.ValueObjects;
using FoodChallenge.Order.Domain.Globalization;
using Moq;

namespace FoodChallenge.Order.UnitTests.Clientes.Specifications;

public class ClienteRegistrarSpecificationTests : TestBase
{
    private readonly Faker _faker;
    private readonly Mock<IClienteGateway> _gateway;
    private readonly ClienteRegistrarSpecification _specification;

    public ClienteRegistrarSpecificationTests()
    {
        _faker = new Faker();
        _faker = GetFaker();
        _gateway = new Mock<IClienteGateway>();

        _specification = new ClienteRegistrarSpecification(_gateway.Object);
    }

    [Fact]
    public async Task DeveSerValida_QuandoCpfEhValidoENaoCadastrado()
    {
        // Arrange
        var cliente = new Cliente(_faker.Person.Cpf());

        _gateway.Setup(g => g.ObterPorCpfAsync(cliente.Cpf, It.IsAny<CancellationToken>()))
               .ReturnsAsync((Cliente)null);

        // Act
        var result = await _specification.ValidateModelAsync(cliente, CancellationToken.None);

        // Assert
        Assert.True(result.Valid);
        Assert.Empty(result.Responses);
    }

    [Fact]
    public async Task DeveSerInvalida_QuandoCpfJaExiste()
    {
        // Arrange
        var cpf = _faker.Person.Cpf();
        var cliente = new Cliente(cpf);
        var validationMessages = new List<string>
        {
            string.Format(Textos.RegistroJaCadastrado, nameof(Cliente), nameof(Cliente.Cpf))
        };

        _gateway.Setup(g => g.ObterPorCpfAsync(new Cpf(cpf), It.IsAny<CancellationToken>()))
               .ReturnsAsync(cliente);

        // Act
        var result = await _specification.ValidateModelAsync(cliente, CancellationToken.None);

        // Assert
        Assert.False(result.Valid);
        Assert.Equal(result.Responses.SelectMany(m => m.Mensagens), validationMessages);
    }

    [Fact]
    public async Task DeveSerInvalida_QuandoCpfEhInvalido()
    {
        // Arrange
        var cliente = new Cliente("12345678901");
        var validationMessages = new List<string>
        {
            string.Format(Textos.CampoInvalido, nameof(Cliente.Cpf))
        };

        // Act
        var result = await _specification.ValidateModelAsync(cliente, CancellationToken.None);

        // Assert
        Assert.False(result.Valid);
        Assert.Equal(result.Responses.SelectMany(m => m.Mensagens), validationMessages);
    }

    [Fact]
    public async Task DeveSerValida_QuandoCpfEhNullMasNomeEEmailSaoValidos()
    {
        // Arrange
        var cliente = new Cliente()
        {
            Nome = _faker.Person.FullName,
            Email = new Email(_faker.Person.Email)
        };

        // Act
        var result = await _specification.ValidateModelAsync(cliente, CancellationToken.None);

        // Assert
        Assert.True(result.Valid);
        Assert.Empty(result.Responses);
    }

    [Fact]
    public async Task DeveSerInvalida_QuandoCpfEhNullENomeEhVazio()
    {
        // Arrange
        var cliente = new Cliente()
        {
            Nome = "",
            Email = new Email(_faker.Person.Email)
        };
        var validationMessages = new List<string>
        {
            string.Format(Textos.CampoObrigatorio, nameof(Cliente.Nome))
        };

        // Act
        var result = await _specification.ValidateModelAsync(cliente, CancellationToken.None);

        // Assert
        Assert.False(result.Valid);
        Assert.Equal(result.Responses.SelectMany(m => m.Mensagens), validationMessages);
    }

    [Fact]
    public async Task DeveSerInvalida_QuandoCpfEhNullEEmailEhInvalido()
    {
        // Arrange
        var cliente = new Cliente()
        {
            Nome = _faker.Person.FullName,
            Email = new Email("invalido") // inválido
        };
        var validationMessages = new List<string>
        {
            string.Format(Textos.CampoInvalido, nameof(Cliente.Email))
        };

        // Act
        var result = await _specification.ValidateModelAsync(cliente, CancellationToken.None);

        // Assert
        Assert.False(result.Valid);
        Assert.Equal(result.Responses.SelectMany(m => m.Mensagens), validationMessages);
    }
}
