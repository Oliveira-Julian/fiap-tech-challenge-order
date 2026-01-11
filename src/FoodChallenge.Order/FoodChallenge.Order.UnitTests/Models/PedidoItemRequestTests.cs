using Bogus;
using FoodChallenge.Order.Application.Pedidos.Models.Requests;

namespace FoodChallenge.Order.UnitTests.Models;

public class PedidoItemRequestTests : TestBase
{
    private readonly Faker _faker;

    public PedidoItemRequestTests()
    {
        _faker = GetFaker();
    }

    [Fact]
    public void DevePossuirPropriedadesValidas()
    {
        // Arrange
        var produtoId = Guid.NewGuid();
        var request = new PedidoItemRequest
        {
            IdProduto = produtoId,
            Quantidade = 2
        };

        // Act & Assert
        Assert.Equal(produtoId, request.IdProduto);
        Assert.Equal(2, request.Quantidade);
    }

    [Fact]
    public void DevePermitirIdProdutoValido()
    {
        // Arrange
        var ids = new[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };

        foreach (var id in ids)
        {
            var request = new PedidoItemRequest
            {
                IdProduto = id,
                Quantidade = 1
            };

            // Act & Assert
            Assert.Equal(id, request.IdProduto);
        }
    }

    [Fact]
    public void DevePermitirIdProdutoVazio()
    {
        // Arrange
        var request = new PedidoItemRequest
        {
            IdProduto = Guid.Empty,
            Quantidade = 1
        };

        // Act & Assert
        Assert.Equal(Guid.Empty, request.IdProduto);
    }

    [Fact]
    public void DevePermitirQuantidadePositiva()
    {
        // Arrange
        var quantidades = new[] { 1, 5, 10, 100, 1000 };

        foreach (var quantidade in quantidades)
        {
            var request = new PedidoItemRequest
            {
                IdProduto = Guid.NewGuid(),
                Quantidade = quantidade
            };

            // Act & Assert
            Assert.Equal(quantidade, request.Quantidade);
        }
    }

    [Fact]
    public void DevePermitirQuantidadeZero()
    {
        // Arrange
        var request = new PedidoItemRequest
        {
            IdProduto = Guid.NewGuid(),
            Quantidade = 0
        };

        // Act & Assert
        Assert.Equal(0, request.Quantidade);
    }

    [Fact]
    public void DevePermitirQuantidadeNegativa()
    {
        // Arrange
        var request = new PedidoItemRequest
        {
            IdProduto = Guid.NewGuid(),
            Quantidade = -5
        };

        // Act & Assert
        Assert.Equal(-5, request.Quantidade);
    }

    [Fact]
    public void DevePermitirAlteracaoDeIdProduto()
    {
        // Arrange
        var request = new PedidoItemRequest();
        var novoId = Guid.NewGuid();

        // Act
        request.IdProduto = novoId;

        // Assert
        Assert.Equal(novoId, request.IdProduto);
    }

    [Fact]
    public void DevePermitirAlteracaoDeQuantidade()
    {
        // Arrange
        var request = new PedidoItemRequest();

        // Act
        request.Quantidade = 15;

        // Assert
        Assert.Equal(15, request.Quantidade);
    }

    [Fact]
    public void DeveSerSealedClass()
    {
        // Arrange & Act
        var type = typeof(PedidoItemRequest);

        // Assert
        Assert.True(type.IsSealed);
    }

    [Fact]
    public void DevePermitirQuantidadeGrande()
    {
        // Arrange
        var request = new PedidoItemRequest
        {
            IdProduto = Guid.NewGuid(),
            Quantidade = int.MaxValue
        };

        // Act & Assert
        Assert.Equal(int.MaxValue, request.Quantidade);
    }

    [Fact]
    public void DevePermitirQuantidadeMuitoNegativa()
    {
        // Arrange
        var request = new PedidoItemRequest
        {
            IdProduto = Guid.NewGuid(),
            Quantidade = int.MinValue
        };

        // Act & Assert
        Assert.Equal(int.MinValue, request.Quantidade);
    }
}
