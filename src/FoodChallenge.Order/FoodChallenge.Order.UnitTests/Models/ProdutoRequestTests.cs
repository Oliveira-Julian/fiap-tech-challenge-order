using Bogus;
using FoodChallenge.Order.Application.Produtos.Models.Requests;
using FoodChallenge.Order.Domain.Enums;

namespace FoodChallenge.Order.UnitTests.Models;

public class ProdutoRequestTests : TestBase
{
    private readonly Faker _faker;

    public ProdutoRequestTests()
    {
        _faker = GetFaker();
    }

    [Fact]
    public void DevePossuirPropriedadesValidas()
    {
        // Arrange
        var request = new ProdutoRequest
        {
            Nome = "Hambúrguer",
            Descricao = "Hambúrguer artesanal",
            Preco = 25.50m,
            Categoria = ProdutoCategoria.Lanche
        };

        // Act & Assert
        Assert.NotNull(request);
        Assert.Equal("Hambúrguer", request.Nome);
        Assert.Equal("Hambúrguer artesanal", request.Descricao);
        Assert.Equal(25.50m, request.Preco);
        Assert.Equal(ProdutoCategoria.Lanche, request.Categoria);
    }

    [Fact]
    public void DevePermitirCategoriaAcompanhamento()
    {
        // Arrange
        var request = new ProdutoRequest
        {
            Nome = "Batata Frita",
            Descricao = "Batata crocante",
            Preco = 15.00m,
            Categoria = ProdutoCategoria.Acompanhamento
        };

        // Act & Assert
        Assert.Equal(ProdutoCategoria.Acompanhamento, request.Categoria);
    }

    [Fact]
    public void DevePermitirCategoriaBebida()
    {
        // Arrange
        var request = new ProdutoRequest
        {
            Nome = "Refrigerante",
            Descricao = "Refrigerante gelado",
            Preco = 8.50m,
            Categoria = ProdutoCategoria.Bebida
        };

        // Act & Assert
        Assert.Equal(ProdutoCategoria.Bebida, request.Categoria);
    }

    [Fact]
    public void DevePermitirCategoriaSobremesa()
    {
        // Arrange
        var request = new ProdutoRequest
        {
            Nome = "Sorvete",
            Descricao = "Sorvete delicioso",
            Preco = 12.00m,
            Categoria = ProdutoCategoria.Sobremsa
        };

        // Act & Assert
        Assert.Equal(ProdutoCategoria.Sobremsa, request.Categoria);
    }

    [Fact]
    public void DevePermitirPrecoDecimal()
    {
        // Arrange
        var precos = new decimal[] { 0.01m, 99.99m, 1000.00m, 0.50m };

        foreach (var preco in precos)
        {
            var request = new ProdutoRequest
            {
                Nome = "Teste",
                Descricao = "Teste",
                Preco = preco,
                Categoria = ProdutoCategoria.Lanche
            };

            // Act & Assert
            Assert.Equal(preco, request.Preco);
        }
    }

    [Fact]
    public void DevePermitirNomeVazio()
    {
        // Arrange
        var request = new ProdutoRequest
        {
            Nome = string.Empty,
            Descricao = "Teste",
            Preco = 10.00m,
            Categoria = ProdutoCategoria.Lanche
        };

        // Act & Assert
        Assert.Empty(request.Nome);
    }

    [Fact]
    public void DevePermitirDescricaoVazia()
    {
        // Arrange
        var request = new ProdutoRequest
        {
            Nome = "Teste",
            Descricao = string.Empty,
            Preco = 10.00m,
            Categoria = ProdutoCategoria.Lanche
        };

        // Act & Assert
        Assert.Empty(request.Descricao);
    }

    [Fact]
    public void DevePermitirNomeNulo()
    {
        // Arrange
        var request = new ProdutoRequest
        {
            Nome = null,
            Descricao = "Teste",
            Preco = 10.00m,
            Categoria = ProdutoCategoria.Lanche
        };

        // Act & Assert
        Assert.Null(request.Nome);
    }

    [Fact]
    public void DevePermitirDescricaoNula()
    {
        // Arrange
        var request = new ProdutoRequest
        {
            Nome = "Teste",
            Descricao = null,
            Preco = 10.00m,
            Categoria = ProdutoCategoria.Lanche
        };

        // Act & Assert
        Assert.Null(request.Descricao);
    }

    [Fact]
    public void DeveSerSealedClass()
    {
        // Arrange & Act
        var type = typeof(ProdutoRequest);

        // Assert
        Assert.True(type.IsSealed);
    }

    [Fact]
    public void DevePermitirAlteracaoDasPropriedades()
    {
        // Arrange
        var request = new ProdutoRequest();

        // Act
        request.Nome = "Novo Nome";
        request.Descricao = "Nova Descricao";
        request.Preco = 99.99m;
        request.Categoria = ProdutoCategoria.Bebida;

        // Assert
        Assert.Equal("Novo Nome", request.Nome);
        Assert.Equal("Nova Descricao", request.Descricao);
        Assert.Equal(99.99m, request.Preco);
        Assert.Equal(ProdutoCategoria.Bebida, request.Categoria);
    }
}
