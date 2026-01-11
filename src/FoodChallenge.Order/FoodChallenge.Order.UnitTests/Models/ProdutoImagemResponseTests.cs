using Bogus;
using FoodChallenge.Order.Application.Produtos.Imagem.Models.Responses;

namespace FoodChallenge.Order.UnitTests.Models;

public class ProdutoImagemResponseTests : TestBase
{
    private readonly Faker _faker;

    public ProdutoImagemResponseTests()
    {
        _faker = GetFaker();
    }

    [Fact]
    public void DevePossuirPropriedadesValidas()
    {
        // Arrange
        var id = Guid.NewGuid();
        var response = new ProdutoImagemResponse
        {
            Id = id,
            Nome = _faker.System.FileName(),
            Tipo = "image/jpeg",
            Tamanho = 512 * 1024,
            Conteudo = new byte[512 * 1024]
        };

        // Act & Assert
        Assert.NotNull(response);
        Assert.Equal(id, response.Id);
        Assert.NotNull(response.Nome);
        Assert.Equal("image/jpeg", response.Tipo);
        Assert.Equal(512 * 1024, response.Tamanho);
        Assert.NotNull(response.Conteudo);
    }

    [Fact]
    public void DevePermitirIdValido()
    {
        // Arrange
        var id = Guid.NewGuid();
        var response = new ProdutoImagemResponse { Id = id };

        // Act & Assert
        Assert.Equal(id, response.Id);
    }

    [Fact]
    public void DevePermitirIdNulo()
    {
        // Arrange
        var response = new ProdutoImagemResponse { Id = null };

        // Act & Assert
        Assert.Null(response.Id);
    }

    [Fact]
    public void DevePermitirNomeValido()
    {
        // Arrange
        var nome = _faker.System.FileName();
        var response = new ProdutoImagemResponse { Nome = nome };

        // Act & Assert
        Assert.Equal(nome, response.Nome);
    }

    [Fact]
    public void DevePermitirNomeNulo()
    {
        // Arrange
        var response = new ProdutoImagemResponse { Nome = null };

        // Act & Assert
        Assert.Null(response.Nome);
    }

    [Fact]
    public void DevePermitirNomeVazio()
    {
        // Arrange
        var response = new ProdutoImagemResponse { Nome = string.Empty };

        // Act & Assert
        Assert.Empty(response.Nome);
    }

    [Fact]
    public void DevePermitirTipoValido()
    {
        // Arrange
        var response = new ProdutoImagemResponse { Tipo = "image/png" };

        // Act & Assert
        Assert.Equal("image/png", response.Tipo);
    }

    [Fact]
    public void DevePermitirTipoNulo()
    {
        // Arrange
        var response = new ProdutoImagemResponse { Tipo = null };

        // Act & Assert
        Assert.Null(response.Tipo);
    }

    [Fact]
    public void DevePermitirTamanhoPositivo()
    {
        // Arrange
        var tamanho = 1024m;
        var response = new ProdutoImagemResponse { Tamanho = tamanho };

        // Act & Assert
        Assert.Equal(tamanho, response.Tamanho);
        Assert.True(response.Tamanho > 0);
    }

    [Fact]
    public void DevePermitirTamanhoZero()
    {
        // Arrange
        var response = new ProdutoImagemResponse { Tamanho = 0 };

        // Act & Assert
        Assert.Equal(0, response.Tamanho);
    }

    [Fact]
    public void DevePermitirConteudoValido()
    {
        // Arrange
        var conteudo = new byte[512 * 1024];
        var response = new ProdutoImagemResponse { Conteudo = conteudo };

        // Act & Assert
        Assert.Equal(conteudo, response.Conteudo);
    }

    [Fact]
    public void DevePermitirConteudoNulo()
    {
        // Arrange
        var response = new ProdutoImagemResponse { Conteudo = null };

        // Act & Assert
        Assert.Null(response.Conteudo);
    }

    [Fact]
    public void DevePermitirConteudoVazio()
    {
        // Arrange
        var response = new ProdutoImagemResponse { Conteudo = [] };

        // Act & Assert
        Assert.NotNull(response.Conteudo);
        Assert.Empty(response.Conteudo);
    }

    [Fact]
    public void DeveSerSealedClass()
    {
        // Arrange & Act
        var type = typeof(ProdutoImagemResponse);

        // Assert
        Assert.True(type.IsSealed);
    }

    [Fact]
    public void DevePermitirAlteracaoDeTodasAsPropriedades()
    {
        // Arrange
        var response = new ProdutoImagemResponse();
        var id = Guid.NewGuid();
        var nome = _faker.System.FileName();
        var tipo = "image/webp";
        var tamanho = 1024m;
        var conteudo = new byte[1024];

        // Act
        response.Id = id;
        response.Nome = nome;
        response.Tipo = tipo;
        response.Tamanho = tamanho;
        response.Conteudo = conteudo;

        // Assert
        Assert.Equal(id, response.Id);
        Assert.Equal(nome, response.Nome);
        Assert.Equal(tipo, response.Tipo);
        Assert.Equal(tamanho, response.Tamanho);
        Assert.Equal(conteudo, response.Conteudo);
    }

    [Fact]
    public void DevePermitirMultiplosTiposDeImagem()
    {
        // Arrange
        var tipos = new[] { "image/jpeg", "image/png", "image/webp", "image/jpg" };

        foreach (var tipo in tipos)
        {
            var response = new ProdutoImagemResponse { Tipo = tipo };

            // Act & Assert
            Assert.Equal(tipo, response.Tipo);
        }
    }

    [Fact]
    public void DevePermitirTamanhoMuitoGrande()
    {
        // Arrange
        var tamanhoMB = 10m * 1024 * 1024; // 10 MB
        var response = new ProdutoImagemResponse { Tamanho = tamanhoMB };

        // Act & Assert
        Assert.Equal(tamanhoMB, response.Tamanho);
    }
}
