using Bogus;
using FoodChallenge.Order.Application.Produtos.Imagem.Models.Requests;

namespace FoodChallenge.Order.UnitTests.Models;

public class ProdutoImagemRequestTests : TestBase
{
    private readonly Faker _faker;

    public ProdutoImagemRequestTests()
    {
        _faker = GetFaker();
    }

    [Fact]
    public void DevePossuirPropriedadeImagens()
    {
        // Arrange
        var request = new ProdutoImagemRequest();

        // Act & Assert
        Assert.NotNull(typeof(ProdutoImagemRequest).GetProperty(nameof(ProdutoImagemRequest.Imagens)));
    }

    [Fact]
    public void DevePermitirImagensNulas()
    {
        // Arrange
        var request = new ProdutoImagemRequest { Imagens = null };

        // Act & Assert
        Assert.Null(request.Imagens);
    }

    [Fact]
    public void DevePermitirImagensVazias()
    {
        // Arrange
        var request = new ProdutoImagemRequest { Imagens = new List<Microsoft.AspNetCore.Http.IFormFile>() };

        // Act & Assert
        Assert.NotNull(request.Imagens);
        Assert.Empty(request.Imagens);
    }

    [Fact]
    public void DevePermitirAlteracaoDeImagens()
    {
        // Arrange
        var request = new ProdutoImagemRequest();
        var novasImagens = new List<Microsoft.AspNetCore.Http.IFormFile>();

        // Act
        request.Imagens = novasImagens;

        // Assert
        Assert.Equal(novasImagens, request.Imagens);
    }

    [Fact]
    public void DeveSerSealedClass()
    {
        // Arrange & Act
        var type = typeof(ProdutoImagemRequest);

        // Assert
        Assert.True(type.IsSealed);
    }

    [Fact]
    public void DevePermitirConstrutorSemParametros()
    {
        // Arrange & Act
        var request = new ProdutoImagemRequest();

        // Assert
        Assert.NotNull(request);
    }

    [Fact]
    public void DevePermitirInstanciacaoComImagensVazias()
    {
        // Arrange & Act
        var imagens = new List<Microsoft.AspNetCore.Http.IFormFile>();
        var request = new ProdutoImagemRequest { Imagens = imagens };

        // Assert
        Assert.NotNull(request);
        Assert.Empty(request.Imagens);
    }

    [Fact]
    public void DevePermitirMultiplasAlteracoes()
    {
        // Arrange
        var request = new ProdutoImagemRequest();
        var imagens1 = new List<Microsoft.AspNetCore.Http.IFormFile>();
        var imagens2 = new List<Microsoft.AspNetCore.Http.IFormFile>();

        // Act
        request.Imagens = imagens1;
        Assert.Equal(imagens1, request.Imagens);

        request.Imagens = imagens2;
        // Assert
        Assert.Equal(imagens2, request.Imagens);
    }

    [Fact]
    public void DevePermitirDefinicaoComNulo()
    {
        // Arrange
        var request = new ProdutoImagemRequest();
        var imagens = new List<Microsoft.AspNetCore.Http.IFormFile>();

        // Act
        request.Imagens = imagens;
        request.Imagens = null;

        // Assert
        Assert.Null(request.Imagens);
    }

    [Fact]
    public void DeveAceitarIEnumerableDeIFormFile()
    {
        // Arrange
        var request = new ProdutoImagemRequest();
        var imagens = new List<Microsoft.AspNetCore.Http.IFormFile>();
        var enumerable = (System.Collections.Generic.IEnumerable<Microsoft.AspNetCore.Http.IFormFile>)imagens;

        // Act
        request.Imagens = enumerable;

        // Assert
        Assert.Equal(enumerable, request.Imagens);
    }
}
