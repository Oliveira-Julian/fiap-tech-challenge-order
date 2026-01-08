using Bogus;
using FoodChallenge.Order.Application.Pedidos;
using FoodChallenge.Order.Application.Pedidos.UseCases;
using FoodChallenge.Order.Domain.Pedidos;
using Moq;

namespace FoodChallenge.Order.UnitTests.Pedidos.UseCases;

public class ObtemPedidoUseCaseTests
{
    private readonly Mock<IPedidoGateway> _pedidoGateway;
    private readonly ObtemPedidoUseCase _useCase;
    private readonly Faker _faker;

    public ObtemPedidoUseCaseTests()
    {
        _pedidoGateway = new Mock<IPedidoGateway>();
        _useCase = new ObtemPedidoUseCase(_pedidoGateway.Object);
        _faker = new Faker();
    }

    [Fact]
    public async Task DeveRetornarPedido_QuandoEncontrado()
    {
        // Arrange
        var id = Guid.NewGuid();
        var pedido = new Pedido { Id = id };

        _pedidoGateway.Setup(g => g.ObterPedidoComRelacionamentosAsync(id, It.IsAny<CancellationToken>(), false))
            .ReturnsAsync(pedido);

        // Act
        var result = await _useCase.ExecutarAsync(id, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
        _pedidoGateway.Verify(g => g.ObterPedidoComRelacionamentosAsync(id, It.IsAny<CancellationToken>(), false), Times.Once);
    }

    [Fact]
    public async Task DeveRetornarNull_QuandoPedidoNaoForEncontrado()
    {
        // Arrange
        var id = Guid.NewGuid();

        _pedidoGateway.Setup(g => g.ObterPedidoComRelacionamentosAsync(id, It.IsAny<CancellationToken>(), false))
            .ReturnsAsync((Pedido)null);

        // Act
        var result = await _useCase.ExecutarAsync(id, CancellationToken.None);

        // Assert
        Assert.Null(result);
        _pedidoGateway.Verify(g => g.ObterPedidoComRelacionamentosAsync(id, It.IsAny<CancellationToken>(), false), Times.Once);
    }

    [Fact]
    public async Task DeveLancarExcecao_QuandoGatewayLancarErro()
    {
        // Arrange
        var id = Guid.NewGuid();
        _pedidoGateway.Setup(g => g.ObterPedidoComRelacionamentosAsync(id, It.IsAny<CancellationToken>(), false))
            .Throws(new Exception("Erro ao obter pedido"));

        // Act & Assert
        var ex = await Assert.ThrowsAsync<Exception>(() =>
            _useCase.ExecutarAsync(id, CancellationToken.None));

        Assert.Equal("Erro ao obter pedido", ex.Message);
        _pedidoGateway.Verify(g => g.ObterPedidoComRelacionamentosAsync(id, It.IsAny<CancellationToken>(), false), Times.Once);
    }
}
