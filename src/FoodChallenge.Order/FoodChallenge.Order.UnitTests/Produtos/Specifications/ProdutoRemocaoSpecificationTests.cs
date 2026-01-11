using Bogus;
using FoodChallenge.Order.Application.Pedidos;
using FoodChallenge.Order.Application.Produtos.Specifications;
using FoodChallenge.Order.Domain.Enums;
using FoodChallenge.Order.Domain.Globalization;
using FoodChallenge.Order.Domain.Pedidos;
using FoodChallenge.Order.UnitTests.Mocks;
using Moq;

namespace FoodChallenge.Order.UnitTests.Produtos.Specifications;

public class ProdutoRemocaoSpecificationTests : TestBase
{
    private readonly Faker _faker;
    private readonly Mock<IPedidoGateway> _pedidoGateway;
    private readonly ProdutoRemocaoSpecification _specification;

    public ProdutoRemocaoSpecificationTests()
    {
        _faker = GetFaker();
        _pedidoGateway = new Mock<IPedidoGateway>();
        _specification = new ProdutoRemocaoSpecification(_pedidoGateway.Object);
    }

    [Fact]
    public async Task DeveSerValida_QuandoProdutoNaoTemPedidosAtivos()
    {
        // Arrange
        var produto = ProdutoMock.CriarValido();
        var produtoId = produto.Id.Value;

        _pedidoGateway.Setup(x => x.ObterPedidosPorProdutoAsync(
            It.IsAny<Guid>(),
            It.IsAny<IEnumerable<PedidoStatus>>(),
            It.IsAny<CancellationToken>(),
            It.IsAny<bool>()))
            .ReturnsAsync(new List<Pedido>());

        // Act
        var result = await _specification.ValidateModelAsync(produto, CancellationToken.None);

        // Assert
        Assert.True(result.Valid);
        Assert.Empty(result.Responses);
    }

    [Fact]
    public async Task DeveSerValida_QuandoProdutoTemPedidosFinalizados()
    {
        // Arrange
        var produto = ProdutoMock.CriarValido();
        var pedidoFinalizado = PedidoMock.CriarValido();
        pedidoFinalizado.Status = PedidoStatus.Finalizado;

        _pedidoGateway.Setup(x => x.ObterPedidosPorProdutoAsync(
            It.IsAny<Guid>(),
            It.IsAny<IEnumerable<PedidoStatus>>(),
            It.IsAny<CancellationToken>(),
            It.IsAny<bool>()))
            .ReturnsAsync(new List<Pedido>());

        // Act
        var result = await _specification.ValidateModelAsync(produto, CancellationToken.None);

        // Assert
        Assert.True(result.Valid);
    }

    [Fact]
    public async Task DeveSerInvalida_QuandoProdutoTemPedidosRecebidos()
    {
        // Arrange
        var produto = ProdutoMock.CriarValido();
        var pedidoAtivo = PedidoMock.CriarValido();
        pedidoAtivo.Status = PedidoStatus.Recebido;

        _pedidoGateway.Setup(x => x.ObterPedidosPorProdutoAsync(
            It.IsAny<Guid>(),
            It.IsAny<IEnumerable<PedidoStatus>>(),
            It.IsAny<CancellationToken>(),
            It.IsAny<bool>()))
            .ReturnsAsync(new List<Pedido> { pedidoAtivo });

        // Act
        var result = await _specification.ValidateModelAsync(produto, CancellationToken.None);

        // Assert
        Assert.False(result.Valid);
        var mensagens = result.Responses.SelectMany(m => m.Mensagens).ToList();
        Assert.Contains(Textos.ProdutoRemocaoNaoPermitida, mensagens);
    }

    [Fact]
    public async Task DeveSerInvalida_QuandoProdutoTemPedidosNaFila()
    {
        // Arrange
        var produto = ProdutoMock.CriarValido();
        var pedidoAtivo = PedidoMock.CriarValido();
        pedidoAtivo.Status = PedidoStatus.NaFila;

        _pedidoGateway.Setup(x => x.ObterPedidosPorProdutoAsync(
            It.IsAny<Guid>(),
            It.IsAny<IEnumerable<PedidoStatus>>(),
            It.IsAny<CancellationToken>(),
            It.IsAny<bool>()))
            .ReturnsAsync(new List<Pedido> { pedidoAtivo });

        // Act
        var result = await _specification.ValidateModelAsync(produto, CancellationToken.None);

        // Assert
        Assert.False(result.Valid);
        var mensagens = result.Responses.SelectMany(m => m.Mensagens).ToList();
        Assert.Contains(Textos.ProdutoRemocaoNaoPermitida, mensagens);
    }

    [Fact]
    public async Task DeveSerInvalida_QuandoProdutoTemPedidosEmPreparacao()
    {
        // Arrange
        var produto = ProdutoMock.CriarValido();
        var pedidoAtivo = PedidoMock.CriarValido();
        pedidoAtivo.Status = PedidoStatus.EmPreparacao;

        _pedidoGateway.Setup(x => x.ObterPedidosPorProdutoAsync(
            It.IsAny<Guid>(),
            It.IsAny<IEnumerable<PedidoStatus>>(),
            It.IsAny<CancellationToken>(),
            It.IsAny<bool>()))
            .ReturnsAsync(new List<Pedido> { pedidoAtivo });

        // Act
        var result = await _specification.ValidateModelAsync(produto, CancellationToken.None);

        // Assert
        Assert.False(result.Valid);
        var mensagens = result.Responses.SelectMany(m => m.Mensagens).ToList();
        Assert.Contains(Textos.ProdutoRemocaoNaoPermitida, mensagens);
    }

    [Fact]
    public async Task DeveSerInvalida_QuandoProdutoTemPedidosAguardandoRetirada()
    {
        // Arrange
        var produto = ProdutoMock.CriarValido();
        var pedidoAtivo = PedidoMock.CriarValido();
        pedidoAtivo.Status = PedidoStatus.AguardandoRetirada;

        _pedidoGateway.Setup(x => x.ObterPedidosPorProdutoAsync(
            It.IsAny<Guid>(),
            It.IsAny<IEnumerable<PedidoStatus>>(),
            It.IsAny<CancellationToken>(),
            It.IsAny<bool>()))
            .ReturnsAsync(new List<Pedido> { pedidoAtivo });

        // Act
        var result = await _specification.ValidateModelAsync(produto, CancellationToken.None);

        // Assert
        Assert.False(result.Valid);
        var mensagens = result.Responses.SelectMany(m => m.Mensagens).ToList();
        Assert.Contains(Textos.ProdutoRemocaoNaoPermitida, mensagens);
    }

    [Fact]
    public async Task DeveSerInvalida_QuandoProdutoTemMultiplosPedidosAtivos()
    {
        // Arrange
        var produto = ProdutoMock.CriarValido();
        var pedidosAtivos = new List<Pedido>
        {
            PedidoMock.CriarValido(),
            PedidoMock.CriarValido(),
            PedidoMock.CriarValido()
        };

        _pedidoGateway.Setup(x => x.ObterPedidosPorProdutoAsync(
            It.IsAny<Guid>(),
            It.IsAny<IEnumerable<PedidoStatus>>(),
            It.IsAny<CancellationToken>(),
            It.IsAny<bool>()))
            .ReturnsAsync(pedidosAtivos);

        // Act
        var result = await _specification.ValidateModelAsync(produto, CancellationToken.None);

        // Assert
        Assert.False(result.Valid);
        var mensagens = result.Responses.SelectMany(m => m.Mensagens).ToList();
        Assert.Contains(Textos.ProdutoRemocaoNaoPermitida, mensagens);
    }

    [Fact]
    public async Task DeveVerificarPedidosPorProdutoId()
    {
        // Arrange
        var produto = ProdutoMock.CriarValido();
        var produtoId = produto.Id.Value;

        _pedidoGateway.Setup(x => x.ObterPedidosPorProdutoAsync(
            It.IsAny<Guid>(),
            It.IsAny<IEnumerable<PedidoStatus>>(),
            It.IsAny<CancellationToken>(),
            It.IsAny<bool>()))
            .ReturnsAsync(new List<Pedido>());

        // Act
        await _specification.ValidateModelAsync(produto, CancellationToken.None);

        // Assert
        _pedidoGateway.Verify(x => x.ObterPedidosPorProdutoAsync(
            It.IsAny<Guid>(),
            It.IsAny<IEnumerable<PedidoStatus>>(),
            It.IsAny<CancellationToken>(),
            It.IsAny<bool>()), Times.Once);
    }

    [Fact]
    public async Task DeveSerValida_QuandoGatewayRetornaNull()
    {
        // Arrange
        var produto = ProdutoMock.CriarValido();

        _pedidoGateway.Setup(x => x.ObterPedidosPorProdutoAsync(
            It.IsAny<Guid>(),
            It.IsAny<IEnumerable<PedidoStatus>>(),
            It.IsAny<CancellationToken>(),
            It.IsAny<bool>()))
            .ReturnsAsync((List<Pedido>)null);

        // Act
        var result = await _specification.ValidateModelAsync(produto, CancellationToken.None);

        // Assert
        Assert.True(result.Valid);
    }
}
