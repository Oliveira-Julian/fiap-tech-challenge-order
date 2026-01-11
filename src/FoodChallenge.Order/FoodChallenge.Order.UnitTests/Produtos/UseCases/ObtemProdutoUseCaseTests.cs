using Bogus;
using FoodChallenge.Order.Application.Produtos;
using FoodChallenge.Order.Application.Produtos.UseCases;
using FoodChallenge.Order.Domain.Produtos;
using FoodChallenge.Order.UnitTests.Mocks;
using Moq;

namespace FoodChallenge.Order.UnitTests.Produtos.UseCases;

public class ObtemProdutoUseCaseTests : TestBase
{
    private readonly Faker _faker;
    private readonly Mock<IProdutoGateway> _produtoGateway;
    private readonly ObtemProdutoUseCase _useCase;

    public ObtemProdutoUseCaseTests()
    {
        _faker = GetFaker();
        _produtoGateway = new Mock<IProdutoGateway>();

        _useCase = new ObtemProdutoUseCase(_produtoGateway.Object);
    }

    [Fact]
    public async Task DeveObterProduto_QuandoIdEhValido()
    {
        // Arrange
        var produto = ProdutoMock.CriarValido();

        _produtoGateway.Setup(x => x.ObterProdutoPorIdComImagensAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>(), It.IsAny<bool>()))
            .ReturnsAsync(produto);

        // Act
        var result = await _useCase.ExecutarAsync(produto.Id.Value, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(produto.Id, result.Id);
        _produtoGateway.Verify(x => x.ObterProdutoPorIdComImagensAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>(), It.IsAny<bool>()), Times.Once);
    }

    [Fact]
    public async Task DeveRetornarNull_QuandoProdutoNaoExiste()
    {
        // Arrange
        var produtoId = Guid.NewGuid();

        _produtoGateway.Setup(x => x.ObterProdutoPorIdComImagensAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>(), It.IsAny<bool>()))
            .ReturnsAsync((Produto)null);

        // Act
        var result = await _useCase.ExecutarAsync(produtoId, CancellationToken.None);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task DeveLancarExcecao_QuandoGatewayThrow()
    {
        // Arrange
        var produtoId = Guid.NewGuid();

        _produtoGateway.Setup(x => x.ObterProdutoPorIdComImagensAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>(), It.IsAny<bool>()))
            .ThrowsAsync(new Exception("Erro ao obter produto"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _useCase.ExecutarAsync(produtoId, CancellationToken.None));
    }

    [Fact]
    public async Task DeveCarregarProdutoComImagens()
    {
        // Arrange
        var produto = ProdutoMock.CriarValido();

        _produtoGateway.Setup(x => x.ObterProdutoPorIdComImagensAsync(produto.Id.Value, It.IsAny<CancellationToken>(), It.IsAny<bool>()))
            .ReturnsAsync(produto);

        // Act
        var result = await _useCase.ExecutarAsync(produto.Id.Value, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(produto.Nome, result.Nome);
    }
}
