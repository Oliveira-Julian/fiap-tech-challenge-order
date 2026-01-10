using Bogus;
using FoodChallenge.CrossCutting.Paging;
using FoodChallenge.Order.Application.Pedidos;
using FoodChallenge.Order.Application.Pedidos.UseCases;
using FoodChallenge.Order.Domain.Pedidos;
using FoodChallenge.Order.UnitTests.Mocks;
using Moq;

namespace FoodChallenge.Order.UnitTests.Pedidos.UseCases;

public class PesquisaPedidoUseCaseTests : TestBase
{
    private readonly Mock<IPedidoGateway> _pedidoGateway;
    private readonly PesquisaPedidoUseCase _useCase;
    private readonly Faker _faker;

    public PesquisaPedidoUseCaseTests()
    {
        _pedidoGateway = new Mock<IPedidoGateway>();
        _useCase = new PesquisaPedidoUseCase(_pedidoGateway.Object);
        _faker = GetFaker();
    }

    [Fact]
    public async Task DeveRetornarPedidos_QuandoFiltroForValido()
    {
        // Arrange
        var filterData = new PedidoFilter
        {
            Id = Guid.NewGuid(),
            IdCliente = Guid.NewGuid(),
            Ativo = true
        };

        var filtro = new Filter<PedidoFilter>(1, 10, "Id", true, filterData);

        var pedidos = PedidoMock.CriarListaValida(2);

        var pagination = new Pagination<Pedido>(page: 1, sizePage: 10, totalRecords: 2, pedidos);

        _pedidoGateway
            .Setup(x => x.ObterPedidosPorFiltroAsync(filtro, It.IsAny<CancellationToken>()))
            .ReturnsAsync(pagination);

        // Act
        var result = await _useCase.ExecutarAsync(filtro, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.TotalRecords);
        Assert.Equal(2, result.Records.Count());
    }

    [Fact]
    public async Task DeveLancarExcecao_QuandoGatewayLancarErro()
    {
        // Arrange
        var filtro = new Filter<PedidoFilter>(1, 10, new PedidoFilter());

        _pedidoGateway
            .Setup(x => x.ObterPedidosPorFiltroAsync(filtro, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Erro interno"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _useCase.ExecutarAsync(filtro, CancellationToken.None));
    }
}
