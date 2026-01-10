using Bogus;
using Bogus.Extensions.Brazil;
using FoodChallenge.Common.Interfaces;
using FoodChallenge.Common.Validators;
using FoodChallenge.Order.Application.Clientes;
using FoodChallenge.Order.Application.Pagamentos;
using FoodChallenge.Order.Application.Pedidos;
using FoodChallenge.Order.Application.Pedidos.UseCases;
using FoodChallenge.Order.Application.Produtos;
using FoodChallenge.Order.Domain.Clientes;
using FoodChallenge.Order.Domain.Clientes.ValueObjects;
using FoodChallenge.Order.Domain.Pedidos;
using FoodChallenge.Order.UnitTests.Mocks;
using Moq;

namespace FoodChallenge.Order.UnitTests.Pedidos.UseCases;

public class CadastraPedidoUseCaseTests : TestBase
{
    private readonly Faker _faker;
    private readonly ValidationContext _validationContext;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IClienteGateway> _clienteGateway;
    private readonly Mock<IPedidoGateway> _pedidoGateway;
    private readonly Mock<IPagamentoGateway> _pagamentoGateway;
    private readonly Mock<IProdutoGateway> _produtoGateway;
    private readonly CadastraPedidoUseCase _useCase;

    public CadastraPedidoUseCaseTests()
    {
        _faker = GetFaker();
        _validationContext = new ValidationContext();
        _unitOfWork = new Mock<IUnitOfWork>();
        _clienteGateway = new Mock<IClienteGateway>();
        _pedidoGateway = new Mock<IPedidoGateway>();
        _pagamentoGateway = new Mock<IPagamentoGateway>();
        _produtoGateway = new Mock<IProdutoGateway>();

        _useCase = new CadastraPedidoUseCase(
            _validationContext,
            _unitOfWork.Object,
            _clienteGateway.Object,
            _pedidoGateway.Object,
            _pagamentoGateway.Object,
            _produtoGateway.Object
        );
    }

    [Fact]
    public async Task DeveCadastrarPedido_QuandoDadosSaoValidos()
    {
        // Arrange
        var cpf = _faker.Person.Cpf();
        var cliente = new Cliente(cpf);
        var itens = PedidoItemMock.CriarListaValida(2);
        var produtos = itens.Select(i =>
        {
            var produto = ProdutoMock.CriarValido();
            produto.Id = i.IdProduto.Value;
            return produto;
        }).ToList();
        var pedidoCadastrado = new Pedido { Id = Guid.NewGuid() };
        var pagamento = PagamentoMock.CriarValido();

        _clienteGateway.Setup(x => x.ObterPorCpfAsync(It.IsAny<Cpf>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(cliente);

        _produtoGateway.Setup(x => x.ObterProdutosPorIdsAsync(It.IsAny<IEnumerable<Guid?>>(), It.IsAny<CancellationToken>(), true))
            .ReturnsAsync(produtos);

        _pedidoGateway.Setup(x => x.CadastrarPedidoAsync(It.IsAny<Pedido>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(pedidoCadastrado);

        _pagamentoGateway.Setup(x => x.CadastrarPedidoMercadoPagoAsync(It.IsAny<Pedido>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(pagamento);

        _pedidoGateway.Setup(x => x.ObterPedidoComRelacionamentosAsync(pedidoCadastrado.Id.Value, It.IsAny<CancellationToken>(), false))
            .ReturnsAsync(pedidoCadastrado);

        // Act
        var result = await _useCase.ExecutarAsync(cpf, itens, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(pedidoCadastrado, result);
    }

    [Fact]
    public async Task DeveRetornarNull_QuandoValidacaoFalhar()
    {
        // Arrange
        var cpf = _faker.Person.Cpf();
        var cliente = new Cliente(cpf);
        var itens = new List<PedidoItem>();

        _clienteGateway.Setup(x => x.ObterPorCpfAsync(It.IsAny<Cpf>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(cliente);

        // Act
        var result = await _useCase.ExecutarAsync(cpf, itens, CancellationToken.None);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task DeveCadastrarClientePadrao_QuandoCpfEhVazio()
    {
        // Arrange
        var cpf = string.Empty;
        var cliente = new Cliente(cpf);
        var itens = PedidoItemMock.CriarListaValida(2);
        var produtos = itens.Select(i =>
        {
            var produto = ProdutoMock.CriarValido();
            produto.Id = i.IdProduto.Value;
            return produto;
        }).ToList();

        _clienteGateway.Setup(x => x.ObterClientePadraoAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(cliente);

        _produtoGateway.Setup(x => x.ObterProdutosPorIdsAsync(It.IsAny<IEnumerable<Guid?>>(), It.IsAny<CancellationToken>(), true))
            .ReturnsAsync(produtos);

        _pedidoGateway.Setup(x => x.CadastrarPedidoAsync(It.IsAny<Pedido>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Pedido { Id = Guid.NewGuid() });

        _pagamentoGateway.Setup(x => x.CadastrarPedidoMercadoPagoAsync(It.IsAny<Pedido>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(PagamentoMock.CriarValido());

        _pedidoGateway.Setup(x => x.ObterPedidoComRelacionamentosAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>(), false))
            .ReturnsAsync(new Pedido());

        // Act
        var result = await _useCase.ExecutarAsync(cpf, itens, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task DeveCadastrarNovoCliente_QuandoNaoEncontradoPorCpf()
    {
        // Arrange
        var cpf = _faker.Person.Cpf();
        var cliente = new Cliente(cpf);
        var itens = PedidoItemMock.CriarListaValida(1);
        var produto = ProdutoMock.CriarValido();
        produto.Id = itens[0].IdProduto.Value;

        _clienteGateway.Setup(x => x.ObterPorCpfAsync(It.IsAny<Cpf>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Cliente)null);

        _clienteGateway.Setup(x => x.CadastrarClienteAsync(It.IsAny<Cliente>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(cliente);

        _produtoGateway.Setup(x => x.ObterProdutosPorIdsAsync(It.IsAny<IEnumerable<Guid?>>(), It.IsAny<CancellationToken>(), true))
            .ReturnsAsync([produto]);

        _pedidoGateway.Setup(x => x.CadastrarPedidoAsync(It.IsAny<Pedido>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Pedido { Id = Guid.NewGuid() });

        _pagamentoGateway.Setup(x => x.CadastrarPedidoMercadoPagoAsync(It.IsAny<Pedido>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(PagamentoMock.CriarValido());

        _pedidoGateway.Setup(x => x.ObterPedidoComRelacionamentosAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>(), false))
            .ReturnsAsync(new Pedido());

        // Act
        var result = await _useCase.ExecutarAsync(cpf, itens, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        _clienteGateway.Verify(x => x.CadastrarClienteAsync(It.IsAny<Cliente>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeveChamarRollback_QuandoOcorreExcecao()
    {
        // Arrange
        var cpf = _faker.Person.Cpf();
        var cliente = new Cliente(cpf);
        var itens = PedidoItemMock.CriarListaValida(2);
        var produtos = itens.Select(i =>
        {
            var produto = ProdutoMock.CriarValido();
            produto.Id = i.IdProduto.Value;
            return produto;
        }).ToList();

        _clienteGateway.Setup(x => x.ObterPorCpfAsync(It.IsAny<Cpf>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(cliente);

        _produtoGateway.Setup(x => x.ObterProdutosPorIdsAsync(It.IsAny<IEnumerable<Guid?>>(), It.IsAny<CancellationToken>(), true))
            .ReturnsAsync(produtos);

        // Simula falha ao cadastrar o pedido
        _pedidoGateway.Setup(x => x.CadastrarPedidoAsync(It.IsAny<Pedido>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Erro"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _useCase.ExecutarAsync(cpf, itens, CancellationToken.None));
        _unitOfWork.Verify(x => x.RollbackAsync(), Times.Once);
    }

}
