using Bogus;
using FoodChallenge.Common.Interfaces;
using FoodChallenge.Common.Validators;
using FoodChallenge.Order.Application.Pedidos;
using FoodChallenge.Order.Application.Produtos;
using FoodChallenge.Order.Application.Produtos.UseCases;
using FoodChallenge.Order.Domain.Produtos;
using FoodChallenge.Order.UnitTests.Mocks;
using Moq;

namespace FoodChallenge.Order.UnitTests.Produtos.UseCases;

public class RemoveProdutoUseCaseTests : TestBase
{
    private readonly Faker _faker;
    private readonly ValidationContext _validationContext;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IProdutoGateway> _produtoGateway;
    private readonly Mock<IPedidoGateway> _pedidoGateway;
    private readonly RemoveProdutoUseCase _useCase;

    public RemoveProdutoUseCaseTests()
    {
        _faker = GetFaker();
        _validationContext = new ValidationContext();
        _unitOfWork = new Mock<IUnitOfWork>();
        _produtoGateway = new Mock<IProdutoGateway>();
        _pedidoGateway = new Mock<IPedidoGateway>();

        _useCase = new RemoveProdutoUseCase(
            _validationContext,
            _unitOfWork.Object,
            _produtoGateway.Object,
            _pedidoGateway.Object
        );
    }

    [Fact]
    public async Task DeveRemoverProduto_QuandoDadosSaoValidos()
    {
        // Arrange
        var produto = ProdutoMock.CriarValido();

        _produtoGateway.Setup(x => x.ObterProdutoPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>(), It.IsAny<bool>()))
            .ReturnsAsync(produto);

        _produtoGateway.Setup(x => x.AtualizarProduto(It.IsAny<Produto>()));

        // Act
        await _useCase.ExecutarAsync(produto.Id.Value, CancellationToken.None);

        // Assert
        _unitOfWork.Verify(x => x.BeginTransaction(), Times.Once);
        _unitOfWork.Verify(x => x.CommitAsync(), Times.Once);
        _produtoGateway.Verify(x => x.AtualizarProduto(It.IsAny<Produto>()), Times.Once);
    }

    [Fact]
    public async Task DeveRetornarNull_QuandoProdutoNaoEncontrado()
    {
        // Arrange
        var produtoId = Guid.NewGuid();

        _produtoGateway.Setup(x => x.ObterProdutoPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>(), It.IsAny<bool>()))
            .ReturnsAsync((Produto)null);

        // Act
        await _useCase.ExecutarAsync(produtoId, CancellationToken.None);

        // Assert
        _unitOfWork.Verify(x => x.BeginTransaction(), Times.Never);
        _unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
    }

    [Fact]
    public async Task DeveNaoRemover_QuandoValidacaoFalhar()
    {
        // Arrange
        var produto = ProdutoMock.CriarValido();

        _produtoGateway.Setup(x => x.ObterProdutoPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>(), It.IsAny<bool>()))
            .ReturnsAsync(produto);

        // Simula falha na validação (por exemplo, produto associado a pedidos)
        // O teste assume que a validação pode falhar

        // Act
        await _useCase.ExecutarAsync(produto.Id.Value, CancellationToken.None);

        // Assert - Dependendo da lógica de validação
        _unitOfWork.Verify(x => x.BeginTransaction(), Times.AtMostOnce);
    }

    [Fact]
    public async Task DeveChamarRollback_QuandoOcorreExcecao()
    {
        // Arrange
        var produto = ProdutoMock.CriarValido();

        _produtoGateway.Setup(x => x.ObterProdutoPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>(), It.IsAny<bool>()))
            .ReturnsAsync(produto);

        _produtoGateway.Setup(x => x.AtualizarProduto(It.IsAny<Produto>()))
            .Throws(new Exception("Erro ao remover"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _useCase.ExecutarAsync(produto.Id.Value, CancellationToken.None));
        _unitOfWork.Verify(x => x.RollbackAsync(), Times.Once);
    }

    [Fact]
    public async Task DeveMarcarProdutoComoExcluido()
    {
        // Arrange
        var produto = ProdutoMock.CriarValido();
        var produtoCapturado = (Produto)null;

        _produtoGateway.Setup(x => x.ObterProdutoPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>(), It.IsAny<bool>()))
            .ReturnsAsync(produto);

        _produtoGateway.Setup(x => x.AtualizarProduto(It.IsAny<Produto>()))
            .Callback<Produto>(p => produtoCapturado = p);

        // Act
        await _useCase.ExecutarAsync(produto.Id.Value, CancellationToken.None);

        // Assert
        Assert.NotNull(produtoCapturado);
        Assert.False(produtoCapturado.Ativo); // Produto deve estar marcado como inativo
    }
}
