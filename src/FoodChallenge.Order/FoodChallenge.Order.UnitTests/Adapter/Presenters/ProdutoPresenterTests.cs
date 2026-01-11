using Bogus;
using FoodChallenge.CrossCutting.Paging;
using FoodChallenge.Order.Adapter.Presenters;
using FoodChallenge.Order.Domain.Enums;
using FoodChallenge.Order.Domain.Produtos;
using FoodChallenge.Order.UnitTests.Mocks;

namespace FoodChallenge.Order.UnitTests.Adapter.Presenters;

public class ProdutoPresenterTests : TestBase
{
    private readonly Faker _faker;

    public ProdutoPresenterTests()
    {
        _faker = GetFaker();
    }

    [Fact]
    public void DeveConverterProdutoParaResponse()
    {
        // Arrange
        var produto = ProdutoMock.CriarValido();

        // Act
        var resultado = ProdutoPresenter.ToResponse(produto);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(produto.Id, resultado.Id);
        Assert.Equal(produto.Nome, resultado.Nome);
        Assert.Equal(produto.Descricao, resultado.Descricao);
        Assert.Equal(produto.Preco, resultado.Preco);
        Assert.Equal(produto.Categoria, resultado.Categoria);
    }

    [Fact]
    public void DeveRetornarNulo_QuandoProdutoEhNulo()
    {
        // Act
        var resultado = ProdutoPresenter.ToResponse((Produto)null);

        // Assert
        Assert.Null(resultado);
    }

    [Fact]
    public void DeveConverterPaginacaoDeProdutos()
    {
        // Arrange
        var produtos = ProdutoMock.CriarListaValida(3);
        var paginacao = new Pagination<Produto>(1, 10, 3, produtos);

        // Act
        var resultado = ProdutoPresenter.ToPaginationResponse(paginacao);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(paginacao.Page, resultado.Page);
        Assert.Equal(paginacao.SizePage, resultado.SizePage);
        Assert.Equal(paginacao.TotalRecords, resultado.TotalRecords);
        Assert.Equal(3, resultado.Records.Count());
    }

    [Fact]
    public void DeveRetornarNulo_QuandoPaginacaoEhNula()
    {
        // Act
        var resultado = ProdutoPresenter.ToPaginationResponse(null);

        // Assert
        Assert.Null(resultado);
    }

    [Fact]
    public void DeveConverterTodosProdutosDaPaginacao()
    {
        // Arrange
        var produtos = ProdutoMock.CriarListaValida(5);
        var paginacao = new Pagination<Produto>(2, 20, 100, produtos);

        // Act
        var resultado = ProdutoPresenter.ToPaginationResponse(paginacao);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(5, resultado.Records.Count());

        var resultados = resultado.Records.ToList();
        for (int i = 0; i < produtos.Count; i++)
        {
            Assert.Equal(produtos[i].Id, resultados[i].Id);
            Assert.Equal(produtos[i].Nome, resultados[i].Nome);
        }
    }

    [Fact]
    public void DevePreservarCategoria()
    {
        // Arrange
        var produto = new Produto
        {
            Id = Guid.NewGuid(),
            Nome = "Teste",
            Descricao = "Teste",
            Preco = 10.00m,
            Categoria = ProdutoCategoria.Bebida,
            Ativo = true,
            DataCriacao = DateTime.UtcNow
        };

        // Act
        var resultado = ProdutoPresenter.ToResponse(produto);

        // Assert
        Assert.Equal(ProdutoCategoria.Bebida, resultado.Categoria);
    }

    [Fact]
    public void DevePreservarPreco()
    {
        // Arrange
        var preco = 99.99m;
        var produto = new Produto
        {
            Id = Guid.NewGuid(),
            Nome = "Teste",
            Descricao = "Teste",
            Preco = preco,
            Categoria = ProdutoCategoria.Lanche,
            Ativo = true,
            DataCriacao = DateTime.UtcNow
        };

        // Act
        var resultado = ProdutoPresenter.ToResponse(produto);

        // Assert
        Assert.Equal(preco, resultado.Preco);
    }

    [Fact]
    public void DeveConverterPaginacaoVazia()
    {
        // Arrange
        var paginacao = new Pagination<Produto>(1, 10, 0, Enumerable.Empty<Produto>());

        // Act
        var resultado = ProdutoPresenter.ToPaginationResponse(paginacao);

        // Assert
        Assert.NotNull(resultado);
        Assert.Empty(resultado.Records);
        Assert.Equal(0, resultado.TotalRecords);
    }

    [Fact]
    public void DeveManterPropriedadesDaPaginacao()
    {
        // Arrange
        var page = 5;
        var sizePage = 25;
        var totalRecords = 125L;
        var produtos = ProdutoMock.CriarListaValida(25);
        var paginacao = new Pagination<Produto>(page, sizePage, totalRecords, produtos);

        // Act
        var resultado = ProdutoPresenter.ToPaginationResponse(paginacao);

        // Assert
        Assert.Equal(page, resultado.Page);
        Assert.Equal(sizePage, resultado.SizePage);
        Assert.Equal(totalRecords, resultado.TotalRecords);
    }

    [Fact]
    public void DeveConverterProdutoComTodasAsCategorias()
    {
        // Arrange
        var categorias = new[] {
            ProdutoCategoria.Lanche,
            ProdutoCategoria.Acompanhamento,
            ProdutoCategoria.Bebida,
            ProdutoCategoria.Sobremsa
        };

        foreach (var categoria in categorias)
        {
            var produto = new Produto
            {
                Id = Guid.NewGuid(),
                Nome = "Teste",
                Descricao = "Teste",
                Preco = 10.00m,
                Categoria = categoria,
                Ativo = true,
                DataCriacao = DateTime.UtcNow
            };

            // Act
            var resultado = ProdutoPresenter.ToResponse(produto);

            // Assert
            Assert.Equal(categoria, resultado.Categoria);
        }
    }

    [Fact]
    public void DeveConverterProdutoSemImagens()
    {
        // Arrange
        var produto = ProdutoMock.CriarValido();
        produto.Imagens = null;

        // Act
        var resultado = ProdutoPresenter.ToResponse(produto);

        // Assert
        Assert.NotNull(resultado);
        Assert.Null(resultado.Imagens);
    }

    [Fact]
    public void DeveIncluirIdNaResponse()
    {
        // Arrange
        var produtoId = Guid.NewGuid();
        var produto = new Produto
        {
            Id = produtoId,
            Nome = "Teste",
            Descricao = "Teste",
            Preco = 10.00m,
            Categoria = ProdutoCategoria.Lanche,
            Ativo = true,
            DataCriacao = DateTime.UtcNow
        };

        // Act
        var resultado = ProdutoPresenter.ToResponse(produto);

        // Assert
        Assert.Equal(produtoId, resultado.Id);
    }
}
