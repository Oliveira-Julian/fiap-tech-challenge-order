using Bogus;
using FoodChallenge.Order.Application.Produtos.Models.Reponses;
using FoodChallenge.Order.Domain.Enums;

namespace FoodChallenge.Order.UnitTests.Models;

public class ProdutoResponseTests : TestBase
{
    private readonly Faker _faker;

    public ProdutoResponseTests()
    {
        _faker = GetFaker();
    }

    [Fact]
    public void DevePossuirPropriedadesValidas()
    {
        // Arrange
        var id = Guid.NewGuid();
        var categoria = ProdutoCategoria.Lanche;
        var response = new ProdutoResponse
        {
            Id = id,
            Categoria = categoria,
            Nome = _faker.Commerce.ProductName(),
            Descricao = _faker.Lorem.Sentence(),
            Preco = (decimal)_faker.Random.Double(0, 1000),
            Imagens = new List<FoodChallenge.Order.Application.Produtos.Imagem.Models.Responses.ProdutoImagemResponse>()
        };

        // Act & Assert
        Assert.NotNull(response);
        Assert.Equal(id, response.Id);
        Assert.Equal(categoria, response.Categoria);
        Assert.NotNull(response.Nome);
        Assert.NotNull(response.Descricao);
        Assert.True(response.Preco >= 0);
        Assert.NotNull(response.Imagens);
    }

    [Fact]
    public void DevePermitirIdValido()
    {
        // Arrange
        var id = Guid.NewGuid();
        var response = new ProdutoResponse { Id = id };

        // Act & Assert
        Assert.Equal(id, response.Id);
    }

    [Fact]
    public void DevePermitirIdNulo()
    {
        // Arrange
        var response = new ProdutoResponse { Id = null };

        // Act & Assert
        Assert.Null(response.Id);
    }

    [Fact]
    public void DevePermitirTodasAsCategorias()
    {
        // Arrange
        var categorias = new[]
        {
            ProdutoCategoria.Lanche,
            ProdutoCategoria.Acompanhamento,
            ProdutoCategoria.Bebida,
            ProdutoCategoria.Sobremsa
        };

        foreach (var categoria in categorias)
        {
            var response = new ProdutoResponse { Categoria = categoria };

            // Act & Assert
            Assert.Equal(categoria, response.Categoria);
        }
    }

    [Fact]
    public void DevePermitirNomeValido()
    {
        // Arrange
        var nome = _faker.Commerce.ProductName();
        var response = new ProdutoResponse { Nome = nome };

        // Act & Assert
        Assert.Equal(nome, response.Nome);
    }

    [Fact]
    public void DevePermitirNomeNulo()
    {
        // Arrange
        var response = new ProdutoResponse { Nome = null };

        // Act & Assert
        Assert.Null(response.Nome);
    }

    [Fact]
    public void DevePermitirDescricaoValida()
    {
        // Arrange
        var descricao = _faker.Lorem.Sentence();
        var response = new ProdutoResponse { Descricao = descricao };

        // Act & Assert
        Assert.Equal(descricao, response.Descricao);
    }

    [Fact]
    public void DevePermitirDescricaoNula()
    {
        // Arrange
        var response = new ProdutoResponse { Descricao = null };

        // Act & Assert
        Assert.Null(response.Descricao);
    }

    [Fact]
    public void DevePermitirPrecoPositivo()
    {
        // Arrange
        var preco = (decimal)_faker.Random.Double(0.01, 1000);
        var response = new ProdutoResponse { Preco = preco };

        // Act & Assert
        Assert.Equal(preco, response.Preco);
        Assert.True(response.Preco > 0);
    }

    [Fact]
    public void DevePermitirPrecoZero()
    {
        // Arrange
        var response = new ProdutoResponse { Preco = 0 };

        // Act & Assert
        Assert.Equal(0, response.Preco);
    }

    [Fact]
    public void DevePermitirImagensVazias()
    {
        // Arrange
        var response = new ProdutoResponse { Imagens = new List<FoodChallenge.Order.Application.Produtos.Imagem.Models.Responses.ProdutoImagemResponse>() };

        // Act & Assert
        Assert.NotNull(response.Imagens);
        Assert.Empty(response.Imagens);
    }

    [Fact]
    public void DevePermitirImagensNulas()
    {
        // Arrange
        var response = new ProdutoResponse { Imagens = null };

        // Act & Assert
        Assert.Null(response.Imagens);
    }

    [Fact]
    public void DeveSerSealedClass()
    {
        // Arrange & Act
        var type = typeof(ProdutoResponse);

        // Assert
        Assert.True(type.IsSealed);
    }

    [Fact]
    public void DevePermitirAlteracaoDePropriedades()
    {
        // Arrange
        var response = new ProdutoResponse();
        var id = Guid.NewGuid();
        var categoria = ProdutoCategoria.Sobremsa;
        var nome = _faker.Commerce.ProductName();
        var preco = (decimal)_faker.Random.Double(1, 100);

        // Act
        response.Id = id;
        response.Categoria = categoria;
        response.Nome = nome;
        response.Preco = preco;

        // Assert
        Assert.Equal(id, response.Id);
        Assert.Equal(categoria, response.Categoria);
        Assert.Equal(nome, response.Nome);
        Assert.Equal(preco, response.Preco);
    }

    [Fact]
    public void DevePermitirPrecoComMuitasDecimais()
    {
        // Arrange
        var preco = 99.99m;
        var response = new ProdutoResponse { Preco = preco };

        // Act & Assert
        Assert.Equal(preco, response.Preco);
    }
}
