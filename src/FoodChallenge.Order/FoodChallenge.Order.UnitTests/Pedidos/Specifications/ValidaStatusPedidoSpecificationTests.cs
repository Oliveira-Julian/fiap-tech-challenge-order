using FoodChallenge.Order.Application.Pedidos.Specifications;
using FoodChallenge.Order.Domain.Enums;
using FoodChallenge.Order.Domain.Globalization;
using FoodChallenge.Order.Domain.Pedidos;
using FoodChallenge.Order.UnitTests;

namespace FoodChallenge.Order.UnitTests.Pedidos.Specifications;

public class ValidaStatusPedidoEnumSpecificationTests : TestBase
{
    private readonly ValidaStatusPedidoEnumSpecification _specification;

    public ValidaStatusPedidoEnumSpecificationTests()
    {
        _specification = new ValidaStatusPedidoEnumSpecification();
    }

    [Theory]
    [InlineData(PedidoStatus.Recebido)]
    [InlineData(PedidoStatus.NaFila)]
    [InlineData(PedidoStatus.EmPreparacao)]
    [InlineData(PedidoStatus.AguardandoRetirada)]
    [InlineData(PedidoStatus.Finalizado)]
    public async Task DeveSerValido_QuandoStatusEhDefinido(PedidoStatus status)
    {
        // Act
        var result = await _specification.ValidateModelAsync(status, CancellationToken.None);

        // Assert
        Assert.True(result.Valid);
        Assert.Empty(result.Responses);
    }

    [Fact]
    public async Task DeveSerInvalido_QuandoStatusNaoEhDefinido()
    {
        // Arrange
        var statusInvalido = (PedidoStatus)999;
        var validationMessages = new List<string> { string.Format(Textos.CampoInvalido, nameof(Pedido.Status)) };

        // Act
        var result = await _specification.ValidateModelAsync(statusInvalido, CancellationToken.None);

        // Assert
        Assert.False(result.Valid);
        Assert.Equal(validationMessages, result.Responses.SelectMany(r => r.Mensagens));
    }
}
