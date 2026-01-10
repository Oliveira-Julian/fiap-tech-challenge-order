using Bogus;
using FoodChallenge.Order.Application.Produtos.Models.Requests;

namespace FoodChallenge.Order.UnitTests.Models;

public class FiltrarProdutoRequestTests : TestBase
{
    private readonly Faker _faker;

    public FiltrarProdutoRequestTests()
    {
        _faker = GetFaker();
    }

    [Fact]
    public void DevePossuirPropriedadesdeFiltro()
    {
        // Arrange
        var request = new FiltrarProdutoRequest
        {
            Page = 1,
            SizePage = 10,
            FieldOrdenation = "Nome",
            OrdenationAsc = true,
            Categorias = new[] { 1, 2 }
        };

        // Act & Assert
        Assert.Equal(1, request.Page);
        Assert.Equal(10, request.SizePage);
        Assert.Equal("Nome", request.FieldOrdenation);
        Assert.True(request.OrdenationAsc);
        Assert.Equal(new[] { 1, 2 }, request.Categorias);
    }

    [Fact]
    public void DeveHerdarDeFilter()
    {
        // Arrange & Act
        var type = typeof(FiltrarProdutoRequest);
        var baseType = type.BaseType;

        // Assert
        Assert.NotNull(baseType);
        Assert.Equal("Filter", baseType.Name);
    }

    [Fact]
    public void DeveInicializarComPaginaoPadrao()
    {
        // Arrange & Act
        var request = new FiltrarProdutoRequest();

        // Assert
        Assert.Equal(1, request.Page);
        Assert.Equal(10, request.SizePage);
    }

    [Fact]
    public void DevePermitirAlteracaoDePaginacao()
    {
        // Arrange
        var request = new FiltrarProdutoRequest();

        // Act
        request.Page = 5;
        request.SizePage = 25;

        // Assert
        Assert.Equal(5, request.Page);
        Assert.Equal(25, request.SizePage);
    }

    [Fact]
    public void DevePermitirAlteracaoDeOrdenacao()
    {
        // Arrange
        var request = new FiltrarProdutoRequest();

        // Act
        request.FieldOrdenation = "Preco";
        request.OrdenationAsc = false;

        // Assert
        Assert.Equal("Preco", request.FieldOrdenation);
        Assert.False(request.OrdenationAsc);
    }

    [Fact]
    public void DevePermitirListaDeCategorias()
    {
        // Arrange
        var categorias = new[] { 1, 3 };
        var request = new FiltrarProdutoRequest
        {
            Categorias = categorias
        };

        // Act & Assert
        Assert.Equal(categorias, request.Categorias);
    }

    [Fact]
    public void DevePermitirListaDeCategoriasVazia()
    {
        // Arrange
        var request = new FiltrarProdutoRequest
        {
            Categorias = Enumerable.Empty<int>()
        };

        // Act & Assert
        Assert.Empty(request.Categorias);
    }

    [Fact]
    public void DevePermitirListaDeCategoriasNula()
    {
        // Arrange
        var request = new FiltrarProdutoRequest
        {
            Categorias = null
        };

        // Act & Assert
        Assert.Null(request.Categorias);
    }

    [Fact]
    public void DevePermitirMultiplasCategorias()
    {
        // Arrange
        var categorias = new[] { 1, 2, 3, 4 };
        var request = new FiltrarProdutoRequest
        {
            Categorias = categorias
        };

        // Act & Assert
        Assert.Equal(4, request.Categorias.Count());
    }

    [Fact]
    public void DevePermitirFieldOrdenationNulo()
    {
        // Arrange
        var request = new FiltrarProdutoRequest
        {
            FieldOrdenation = null
        };

        // Act & Assert
        Assert.Null(request.FieldOrdenation);
    }

    [Fact]
    public void DevePermitirPaginasGrandes()
    {
        // Arrange
        var request = new FiltrarProdutoRequest();

        // Act
        request.Page = 1000;
        request.SizePage = 500;

        // Assert
        Assert.Equal(1000, request.Page);
        Assert.Equal(500, request.SizePage);
    }

    [Fact]
    public void DeveSerSealedClass()
    {
        // Arrange & Act
        var type = typeof(FiltrarProdutoRequest);

        // Assert
        Assert.True(type.IsSealed);
    }
}
