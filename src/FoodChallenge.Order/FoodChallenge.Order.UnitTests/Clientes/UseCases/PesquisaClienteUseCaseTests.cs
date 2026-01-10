using Bogus;
using Bogus.Extensions.Brazil;
using FoodChallenge.CrossCutting.Paging;
using FoodChallenge.Order.Application.Clientes;
using FoodChallenge.Order.Application.Clientes.UseCases;
using FoodChallenge.Order.Domain.Clientes;
using Moq;

namespace FoodChallenge.Order.UnitTests.Clientes.UseCases;

public class PesquisaClienteUseCaseTests : TestBase
{
    private readonly Faker _faker;
    private readonly Mock<IClienteGateway> _gateway;
    private readonly CancellationToken _cancellationToken;
    private readonly PesquisaClienteUseCase _useCase;

    public PesquisaClienteUseCaseTests()
    {
        _faker = new Faker();
        _faker = GetFaker();
        _gateway = new Mock<IClienteGateway>();
        _cancellationToken = new CancellationToken();

        _useCase = new PesquisaClienteUseCase(_gateway.Object);
    }

    [Fact]
    public async Task ExecutarAsync_DeveRetornarResultadoDoGateway()
    {
        // Arrange
        var filtro = new Filter<ClienteFilter>
        {
            Page = 1,
            SizePage = 10,
            Fields = new ClienteFilter()
            {
                Cpf = _faker.Person.Cpf(),
                Ativo = true
            }
        };
        var clientes = new List<Cliente> { new(_faker.Person.Cpf()), new(_faker.Person.Cpf()) };

        var esperado = new Pagination<Cliente>(filtro.Page, clientes.Count, clientes.Count, clientes);

        _gateway
            .Setup(g => g.ObterClienteAsync(filtro, _cancellationToken))
            .ReturnsAsync(esperado);

        // Act
        var resultado = await _useCase.ExecutarAsync(filtro, _cancellationToken);

        // Assert
        Assert.Same(esperado, resultado);
        _gateway.Verify(g => g.ObterClienteAsync(filtro, _cancellationToken), Times.Once);
    }

    [Fact]
    public async Task ExecutarAsync_QuandoGatewayLancar_DeveRethrow()
    {
        // Arrange
        var filtro = new Filter<ClienteFilter>
        {
            Page = 1,
            SizePage = 10,
            Fields = new ClienteFilter()
        };

        var esperado = new InvalidOperationException("Erro ao consultar clientes");

        _gateway
            .Setup(g => g.ObterClienteAsync(filtro, _cancellationToken))
            .ThrowsAsync(esperado);

        // Act + Assert
        var ex = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _useCase.ExecutarAsync(filtro, _cancellationToken));

        Assert.Same(esperado, ex);
        _gateway.Verify(g => g.ObterClienteAsync(filtro, _cancellationToken), Times.Once);
    }
}
