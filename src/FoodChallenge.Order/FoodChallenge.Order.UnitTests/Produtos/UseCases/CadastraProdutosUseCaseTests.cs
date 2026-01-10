using Bogus;
using FoodChallenge.Common.Interfaces;
using FoodChallenge.Common.Validators;
using FoodChallenge.Order.Application.Produtos;
using FoodChallenge.Order.Application.Produtos.UseCases;
using FoodChallenge.Order.Domain.Produtos;
using FoodChallenge.Order.UnitTests.Mocks;
using Moq;

namespace FoodChallenge.Order.UnitTests.Produtos.UseCases;

public class CadastraProdutosUseCaseTests : TestBase
{
    private readonly Faker _faker;
    private readonly ValidationContext _validationContext;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IProdutoGateway> _produtoGateway;
    private readonly CadastraProdutosUseCase _useCase;

    public CadastraProdutosUseCaseTests()
    {
        _faker = GetFaker();
        _validationContext = new ValidationContext();
        _unitOfWork = new Mock<IUnitOfWork>();
        _produtoGateway = new Mock<IProdutoGateway>();

        _useCase = new CadastraProdutosUseCase(
            _validationContext,
            _unitOfWork.Object,
            _produtoGateway.Object
        );
    }

    [Fact]
    public async Task DeveCadastrarProdutos_QuandoDadosSaoValidos()
    {
        // Arrange
        var produtos = ProdutoMock.CriarListaValida(3);
        var produtosArray = produtos.ToArray();

        _produtoGateway.Setup(x => x.CadastrarProdutosAsync(It.IsAny<Produto[]>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _useCase.ExecutarAsync(produtosArray, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(produtosArray.Length, result.Count());
        _unitOfWork.Verify(x => x.BeginTransaction(), Times.Once);
        _unitOfWork.Verify(x => x.CommitAsync(), Times.Once);
        _produtoGateway.Verify(x => x.CadastrarProdutosAsync(It.IsAny<Produto[]>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeveRetornarNull_QuandoValidacaoFalhar()
    {
        // Arrange
        var produto = ProdutoMock.CriarValido();
        produto.Nome = string.Empty; // Isso deve falhar na validação

        // Act
        var result = await _useCase.ExecutarAsync(new[] { produto }, CancellationToken.None);

        // Assert
        Assert.Null(result);
        _unitOfWork.Verify(x => x.BeginTransaction(), Times.Never);
        _unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
    }

    [Fact]
    public async Task DeveCadastrarMultiplosProdutos()
    {
        // Arrange
        var produtos = ProdutoMock.CriarListaValida(5);
        var produtosArray = produtos.ToArray();

        _produtoGateway.Setup(x => x.CadastrarProdutosAsync(It.IsAny<Produto[]>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _useCase.ExecutarAsync(produtosArray, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(5, result.Count());
    }

    [Fact]
    public async Task DeveChamarRollback_QuandoOcorreExcecao()
    {
        // Arrange
        var produtos = ProdutoMock.CriarListaValida(2);
        var produtosArray = produtos.ToArray();

        _produtoGateway.Setup(x => x.CadastrarProdutosAsync(It.IsAny<Produto[]>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Erro ao cadastrar"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _useCase.ExecutarAsync(produtosArray, CancellationToken.None));
        _unitOfWork.Verify(x => x.RollbackAsync(), Times.Once);
    }
}
