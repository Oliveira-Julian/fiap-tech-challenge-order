using Bogus;
using FoodChallenge.CrossCutting.Paging;
using FoodChallenge.Order.Application.Produtos;
using FoodChallenge.Order.Application.Produtos.UseCases;
using FoodChallenge.Order.Domain.Produtos;
using FoodChallenge.Order.UnitTests.Mocks;
using Moq;

namespace FoodChallenge.Order.UnitTests.Produtos.UseCases;

public class PesquisaProdutoUseCaseTests : TestBase
{
    private readonly Faker _faker;
    private readonly Mock<IProdutoGateway> _produtoGateway;
    private readonly PesquisaProdutoUseCase _useCase;

    public PesquisaProdutoUseCaseTests()
    {
        _faker = GetFaker();
        _produtoGateway = new Mock<IProdutoGateway>();

        _useCase = new PesquisaProdutoUseCase(_produtoGateway.Object);
    }

    [Fact]
    public async Task DevePesquisarProdutos_QuandoFiltroEhValido()
    {
        // Arrange
        var produtos = ProdutoMock.CriarListaValida(5);
        var filtro = new Filter<ProdutoFilter>(1, 10, new ProdutoFilter { Ativo = true });
        var paginacao = new Pagination<Produto>(1, 10, 5, produtos);

        _produtoGateway.Setup(x => x.ObterProdutosPorFiltroAsync(It.IsAny<Filter<ProdutoFilter>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(paginacao);

        // Act
        var result = await _useCase.ExecutarAsync(filtro, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(5, result.Records.Count());
        Assert.Equal(1, result.Page);
        _produtoGateway.Verify(x => x.ObterProdutosPorFiltroAsync(It.IsAny<Filter<ProdutoFilter>>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeveRetornarPaginacaoVazia_QuandoNaoEncontrarProdutos()
    {
        // Arrange
        var filtro = new Filter<ProdutoFilter>(1, 10, new ProdutoFilter { Ativo = true });
        var paginacao = new Pagination<Produto>(1, 10, 0, Enumerable.Empty<Produto>());

        _produtoGateway.Setup(x => x.ObterProdutosPorFiltroAsync(It.IsAny<Filter<ProdutoFilter>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(paginacao);

        // Act
        var result = await _useCase.ExecutarAsync(filtro, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Records);
        Assert.Equal(0, result.TotalRecords);
    }

    [Fact]
    public async Task DevePesquisarProdutosPorPagina()
    {
        // Arrange
        var produtos = ProdutoMock.CriarListaValida(10);
        var filtro = new Filter<ProdutoFilter>(2, 10, new ProdutoFilter { Ativo = true });
        var paginacao = new Pagination<Produto>(2, 10, 50, produtos);

        _produtoGateway.Setup(x => x.ObterProdutosPorFiltroAsync(It.IsAny<Filter<ProdutoFilter>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(paginacao);

        // Act
        var result = await _useCase.ExecutarAsync(filtro, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Page);
        Assert.Equal(10, result.SizePage);
        Assert.Equal(50, result.TotalRecords);
    }

    [Fact]
    public async Task DeveLancarExcecao_QuandoGatewayThrow()
    {
        // Arrange
        var filtro = new Filter<ProdutoFilter>(1, 10, new ProdutoFilter { Ativo = true });

        _produtoGateway.Setup(x => x.ObterProdutosPorFiltroAsync(It.IsAny<Filter<ProdutoFilter>>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Erro ao pesquisar produtos"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _useCase.ExecutarAsync(filtro, CancellationToken.None));
    }

    [Fact]
    public async Task DeveFiltrarProdutosAtivos()
    {
        // Arrange
        var produtos = ProdutoMock.CriarListaValida(3);
        var filtro = new Filter<ProdutoFilter>(1, 10, new ProdutoFilter { Ativo = true });
        var paginacao = new Pagination<Produto>(1, 10, 3, produtos);

        _produtoGateway.Setup(x => x.ObterProdutosPorFiltroAsync(It.IsAny<Filter<ProdutoFilter>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(paginacao);

        // Act
        var result = await _useCase.ExecutarAsync(filtro, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.All(result.Records, p => Assert.True(p.Ativo));
    }
}
