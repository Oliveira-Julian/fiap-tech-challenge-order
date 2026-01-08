using Bogus;
using FoodChallenge.Order.Application.Pedidos.Specifications;
using FoodChallenge.Order.Domain.Enums;
using FoodChallenge.Order.Domain.Globalization;
using FoodChallenge.Order.Domain.Pedidos;
using FoodChallenge.Order.UnitTests;

namespace FoodChallenge.Order.UnitTests.Pedidos.Specifications;

public class AtualizaStatusPedidoSpecificationTests : TestBase
{
    private readonly Faker _faker;

    public AtualizaStatusPedidoSpecificationTests()
    {
        _faker = GetFaker();
    }

    [Theory]
    [InlineData(PedidoStatus.Recebido, PedidoStatus.NaFila)]
    [InlineData(PedidoStatus.NaFila, PedidoStatus.EmPreparacao)]
    [InlineData(PedidoStatus.EmPreparacao, PedidoStatus.AguardandoRetirada)]
    [InlineData(PedidoStatus.AguardandoRetirada, PedidoStatus.Finalizado)]
    public async Task DeveSerValida_QuandoStatusPodeSerAlterado(PedidoStatus pedidoStatus, PedidoStatus pedidoStatusNovo)
    {
        // Arrange
        var pedido = new Pedido();
        pedido.Cadastrar(_faker.Random.Guid());
        pedido.AtualizarStatusPedido(pedidoStatus);

        var specification = new AtualizaStatusPedidoSpecification(pedidoStatusNovo);

        // Act
        var result = await specification.ValidateModelAsync(pedido, CancellationToken.None);

        // Assert
        Assert.True(result.Valid);
        Assert.Empty(result.Responses);
    }

    [Theory]
    [InlineData(PedidoStatus.Recebido, PedidoStatus.AguardandoRetirada)]
    [InlineData(PedidoStatus.NaFila, PedidoStatus.Finalizado)]
    [InlineData(PedidoStatus.EmPreparacao, PedidoStatus.Finalizado)]
    [InlineData(PedidoStatus.AguardandoRetirada, PedidoStatus.EmPreparacao)]
    [InlineData(PedidoStatus.Finalizado, PedidoStatus.NaFila)]
    public async Task DeveSerInvalida_QuandoStatusNaoPodeSerAlterado(PedidoStatus pedidoStatus, PedidoStatus pedidoStatusNovo)
    {
        // Arrange
        var pedido = new Pedido();
        pedido.Cadastrar(_faker.Random.Guid());
        pedido.AtualizarStatusPedido(pedidoStatus);
        var validationMessages = new List<string> { string.Format(Textos.PedidoStatusNaoPermitido, pedido.Status, pedidoStatusNovo) };

        var specification = new AtualizaStatusPedidoSpecification(pedidoStatusNovo);

        // Act
        var result = await specification.ValidateModelAsync(pedido, CancellationToken.None);

        // Assert
        Assert.False(result.Valid);
        Assert.Equal(validationMessages, result.Responses.SelectMany(r => r.Mensagens));
    }
}
