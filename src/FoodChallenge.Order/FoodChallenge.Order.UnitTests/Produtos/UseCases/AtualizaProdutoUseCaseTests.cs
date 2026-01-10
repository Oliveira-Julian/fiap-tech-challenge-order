using Bogus;
using FoodChallenge.Common.Interfaces;
using FoodChallenge.Common.Validators;
using FoodChallenge.Order.Application.Produtos;
using FoodChallenge.Order.Application.Produtos.UseCases;
using FoodChallenge.Order.Domain.Produtos;
using FoodChallenge.Order.UnitTests.Mocks;
using Moq;

namespace FoodChallenge.Order.UnitTests.Produtos.UseCases;

public class AtualizaProdutoUseCaseTests : TestBase
{
    private readonly Faker _faker;
    private readonly ValidationContext _validationContext;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IProdutoGateway> _produtoGateway;
    private readonly AtualizaProdutoUseCase _useCase;

    public AtualizaProdutoUseCaseTests()
    {
        _faker = GetFaker();
        _validationContext = new ValidationContext();
        _unitOfWork = new Mock<IUnitOfWork>();
        _produtoGateway = new Mock<IProdutoGateway>();

        _useCase = new AtualizaProdutoUseCase(
            _validationContext,
            _unitOfWork.Object,
            _produtoGateway.Object
        );
    }

    [Fact]
    public async Task DeveAtualizarProduto_QuandoDadosSaoValidos()
    {
        // Arrange
        var produto = ProdutoMock.CriarValido();
        var produtoExistente = ProdutoMock.CriarValido();
        produtoExistente.Id = produto.Id;

        _produtoGateway.Setup(x => x.ObterProdutoPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>(), It.IsAny<bool>()))
            .ReturnsAsync(produtoExistente);

        _produtoGateway.Setup(x => x.AtualizarProduto(It.IsAny<Produto>()));

        _produtoGateway.Setup(x => x.ObterProdutoPorIdAsync(produto.Id.Value, It.IsAny<CancellationToken>(), It.IsAny<bool>()))
            .ReturnsAsync(produto);

        // Act
        var result = await _useCase.ExecutarAsync(produto, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        _unitOfWork.Verify(x => x.BeginTransaction(), Times.Once);
        _unitOfWork.Verify(x => x.CommitAsync(), Times.Once);
        _produtoGateway.Verify(x => x.AtualizarProduto(It.IsAny<Produto>()), Times.Once);
    }

    [Fact]
    public async Task DeveRetornarNull_QuandoProdutoNaoEncontrado()
    {
        // Arrange
        var produto = ProdutoMock.CriarValido();

        _produtoGateway.Setup(x => x.ObterProdutoPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>(), It.IsAny<bool>()))
            .ReturnsAsync((Produto)null);

        // Act
        var result = await _useCase.ExecutarAsync(produto, CancellationToken.None);

        // Assert
        Assert.Null(result);
        _unitOfWork.Verify(x => x.BeginTransaction(), Times.Never);
        _unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
    }

    [Fact]
    public async Task DeveRetornarNull_QuandoValidacaoFalhar()
    {
        // Arrange
        var produto = ProdutoMock.CriarValido();
        produto.Nome = string.Empty; // Isso deve falhar na validação

        // Act
        var result = await _useCase.ExecutarAsync(produto, CancellationToken.None);

        // Assert
        Assert.Null(result);
        _unitOfWork.Verify(x => x.BeginTransaction(), Times.Never);
    }

    [Fact]
    public async Task DeveChamarRollback_QuandoOcorreExcecao()
    {
        // Arrange
        var produto = ProdutoMock.CriarValido();
        var produtoExistente = ProdutoMock.CriarValido();
        produtoExistente.Id = produto.Id;

        _produtoGateway.Setup(x => x.ObterProdutoPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>(), It.IsAny<bool>()))
            .ReturnsAsync(produtoExistente);

        _produtoGateway.Setup(x => x.AtualizarProduto(It.IsAny<Produto>()))
            .Throws(new Exception("Erro ao atualizar"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _useCase.ExecutarAsync(produto, CancellationToken.None));
        _unitOfWork.Verify(x => x.RollbackAsync(), Times.Once);
    }
}
