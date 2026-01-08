using FoodChallenge.Order.Application.Pedidos.Specifications;
using FoodChallenge.Order.Domain.Globalization;
using FoodChallenge.Order.Domain.Pedidos;
using FoodChallenge.Order.UnitTests;
using FoodChallenge.Order.UnitTests.Mocks;

namespace FoodChallenge.Order.UnitTests.Pedidos.Specifications;

public class CadastraPedidoSpecificationTests : TestBase
{
    public CadastraPedidoSpecificationTests()
    {
    }

    [Fact]
    public async Task DeveSerValida_QuandoTodosProdutosEstaoNoPedido()
    {
        // Arrange
        var itens = PedidoItemMock.CriarListaValida(3);
        var pedido = PedidoMock.CriarValido(itens);
        var ids = itens.Select(i => i.IdProduto.Value);

        var specification = new CadastraPedidoSpecification(ids);

        // Act
        var result = await specification.ValidateModelAsync(pedido, CancellationToken.None);

        // Assert
        Assert.True(result.Valid);
        Assert.Empty(result.Responses);
    }

    [Fact]
    public async Task DeveSerInvalida_QuandoNaoContemTodosProdutos()
    {
        // Arrange
        var itens = PedidoItemMock.CriarListaValida(2);
        var pedido = PedidoMock.CriarValido(itens);
        var produtosEsperados = new[] { itens[0].IdProduto.Value, itens[1].IdProduto.Value, Guid.NewGuid() };
        var validationMessages = new List<string>
        {
            string.Format(Textos.RegistrosNaoEncontrados, "produtos", produtosEsperados.Last())
        };


        var specification = new CadastraPedidoSpecification(produtosEsperados);

        // Act
        var result = await specification.ValidateModelAsync(pedido, CancellationToken.None);

        // Assert
        Assert.False(result.Valid);
        Assert.Equal(validationMessages, result.Responses.SelectMany(r => r.Mensagens));
    }

    [Fact]
    public async Task DeveSerInvalida_QuandoListaDeProdutosEstaVazia()
    {
        // Arrange
        var pedido = PedidoMock.CriarValido();
        var specification = new CadastraPedidoSpecification([]);
        var validationMessages = new List<string>
        {
            string.Format(Textos.CampoObrigatorio, nameof(Pedido.Itens))
        };

        // Act
        var result = await specification.ValidateModelAsync(pedido, CancellationToken.None);

        // Assert
        Assert.False(result.Valid);
        Assert.Equal(validationMessages, result.Responses.SelectMany(r => r.Mensagens));
    }

    [Fact]
    public async Task DeveSerInvalida_QuandoListaDeProdutosEstNula()
    {
        // Arrange
        var pedido = PedidoMock.CriarValido();
        var specification = new CadastraPedidoSpecification(null);
        var validationMessages = new List<string>
        {
            string.Format(Textos.CampoObrigatorio, nameof(Pedido.Itens))
        };

        // Act
        var result = await specification.ValidateModelAsync(pedido, CancellationToken.None);

        // Assert
        Assert.False(result.Valid);
        Assert.Equal(validationMessages, result.Responses.SelectMany(r => r.Mensagens));
    }
}
