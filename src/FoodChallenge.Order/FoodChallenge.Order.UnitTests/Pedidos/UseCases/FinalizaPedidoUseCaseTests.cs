using Bogus;
using FoodChallenge.Common.Interfaces;
using FoodChallenge.Common.Validators;
using FoodChallenge.Order.Application.Pedidos;
using FoodChallenge.Order.Application.Pedidos.UseCases;
using FoodChallenge.Order.Domain.Enums;
using FoodChallenge.Order.Domain.Globalization;
using FoodChallenge.Order.Domain.Pedidos;
using FoodChallenge.Order.UnitTests;
using FoodChallenge.Order.UnitTests.Mocks;
using Moq;

namespace FoodChallenge.Order.UnitTests.Pedidos.UseCases;

public class FinalizaPedidoUseCaseTests : TestBase
{
    private readonly Faker _faker;
    private readonly ValidationContext _validationContext;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IPedidoGateway> _pedidoGateway;
    private readonly FinalizaPedidoUseCase _useCase;

    public FinalizaPedidoUseCaseTests()
    {
        _faker = GetFaker();
        _validationContext = new ValidationContext();
        _unitOfWork = new Mock<IUnitOfWork>();
        _pedidoGateway = new Mock<IPedidoGateway>();

        _useCase = new FinalizaPedidoUseCase(
            _validationContext,
            _unitOfWork.Object,
            _pedidoGateway.Object
        );
    }

    [Fact]
    public async Task DeveRetornarNull_QuandoPedidoNaoEncontrado()
    {
        // Arrange
        var idPedido = Guid.NewGuid();
        _pedidoGateway.Setup(x => x.ObterPedidoComRelacionamentosAsync(idPedido, It.IsAny<CancellationToken>(), false))
            .ReturnsAsync((Pedido)null);

        // Act
        var result = await _useCase.ExecutarAsync(idPedido, CancellationToken.None);

        // Assert
        Assert.Null(result);
        Assert.True(_validationContext.HasValidations);
    }

    [Fact]
    public async Task DeveRetornarNull_QuandoValidacaoFalhar()
    {
        // Arrange
        var pedido = PedidoMock.CriarValido();
        pedido.AtualizarStatusPedido(PedidoStatus.Finalizado);

        var validationMessages = new List<string> { string.Format(Textos.PedidoStatusNaoPermitido, pedido.Status, PedidoStatus.Finalizado) };

        _pedidoGateway.Setup(x => x.ObterPedidoComRelacionamentosAsync(pedido.Id.Value, It.IsAny<CancellationToken>(), false))
            .ReturnsAsync(pedido);

        // Act
        var result = await _useCase.ExecutarAsync(pedido.Id.Value, CancellationToken.None);

        // Assert
        Assert.Null(result);
        Assert.True(_validationContext.HasValidations);
        Assert.Equal(validationMessages, _validationContext.ValidationMessages);
    }

    [Fact]
    public async Task DeveFinalizarPedido_ComSucesso()
    {
        // Arrange
        var pedido = PedidoMock.CriarValido();
        pedido.AtualizarStatusPedido(PedidoStatus.AguardandoRetirada);

        var idPedido = pedido.Id.Value;
        _pedidoGateway.Setup(x => x.ObterPedidoComRelacionamentosAsync(idPedido, It.IsAny<CancellationToken>(), false))
            .ReturnsAsync(pedido);
        _pedidoGateway.Setup(x => x.ObterPedidoAsync(idPedido, It.IsAny<CancellationToken>(), false))
            .ReturnsAsync(pedido);

        // Act
        var result = await _useCase.ExecutarAsync(idPedido, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        _unitOfWork.Verify(x => x.BeginTransaction(), Times.Once);
        _unitOfWork.Verify(x => x.CommitAsync(), Times.Once);
        _pedidoGateway.Verify(x => x.AtualizarPedido(It.Is<Pedido>(p => p.Status == PedidoStatus.Finalizado)), Times.Once);
    }

    [Fact]
    public async Task DeveLancarExcecao_QuandoErroDuranteExecucao()
    {
        // Arrange
        var pedido = PedidoMock.CriarValido();
        pedido.AtualizarStatusPedido(PedidoStatus.AguardandoRetirada);
        _pedidoGateway.Setup(x => x.ObterPedidoComRelacionamentosAsync(pedido.Id.Value, It.IsAny<CancellationToken>(), false))
            .ReturnsAsync(pedido);
        _pedidoGateway.Setup(x => x.AtualizarPedido(It.IsAny<Pedido>()))
            .Throws(new Exception("Erro no banco"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() =>
            _useCase.ExecutarAsync(pedido.Id.Value, CancellationToken.None));

        _unitOfWork.Verify(x => x.RollbackAsync(), Times.Once);
    }
}
