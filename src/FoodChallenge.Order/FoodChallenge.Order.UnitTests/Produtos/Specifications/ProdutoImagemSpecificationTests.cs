using FoodChallenge.Order.Application.Produtos.Imagem.Specifications;
using FoodChallenge.Order.Domain.Globalization;
using FoodChallenge.Order.UnitTests.Mocks;

namespace FoodChallenge.Order.UnitTests.Produtos.Specifications;

public class ProdutoImagemSpecificationTests : TestBase
{
    public ProdutoImagemSpecificationTests()
    {
    }

    [Fact]
    public async Task DeveSerValida_QuandoImagemEhValida()
    {
        // Arrange
        var imagem = ProdutoImagemMock.CriarValido();
        var specification = new ProdutoImagemSpecification();

        // Act
        var result = await specification.ValidateModelAsync(imagem, CancellationToken.None);

        // Assert
        Assert.True(result.Valid);
        Assert.Empty(result.Responses);
    }

    [Fact]
    public async Task DeveSerInvalida_QuandoTipoImagemEhInvalido()
    {
        // Arrange
        var imagem = ProdutoImagemMock.CriarValido();
        imagem.Tipo = "application/pdf";
        var validationMessages = new List<string>
        {
            string.Format(Textos.ImagemTipoInvalido, "image/jpeg, image/jpg, image/png, image/webp")
        };

        var specification = new ProdutoImagemSpecification();

        // Act
        var result = await specification.ValidateModelAsync(imagem, CancellationToken.None);

        // Assert
        Assert.False(result.Valid);
        Assert.Equal(validationMessages, result.Responses.SelectMany(r => r.Mensagens));
    }

    [Fact]
    public async Task DeveSerInvalida_QuandoTamanhoExcedeLimite()
    {
        // Arrange
        var imagem = ProdutoImagemMock.CriarValido();
        imagem.Conteudo = new byte[(int)(1 * 1024 * 1024) + 1]; // 1MB + 1 byte
        var validationMessages = new List<string>
        {
            string.Format(Textos.ImagemTamanhoExcedido, 1)
        };

        var specification = new ProdutoImagemSpecification();

        // Act
        var result = await specification.ValidateModelAsync(imagem, CancellationToken.None);

        // Assert
        Assert.False(result.Valid);
        Assert.Equal(validationMessages, result.Responses.SelectMany(r => r.Mensagens));
    }

}
