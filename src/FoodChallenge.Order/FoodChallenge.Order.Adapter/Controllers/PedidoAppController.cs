using FoodChallenge.Order.Adapter.Mappers;
using FoodChallenge.Order.Adapter.Presenters;
using FoodChallenge.Common.Entities;
using FoodChallenge.Common.Interfaces;
using FoodChallenge.Common.Validators;
using FoodChallenge.Infrastructure.Clients.MercadoPago.Clients;
using FoodChallenge.Infrastructure.Clients.MercadoPago.Settings;
using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Clientes.Interfaces;
using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Pedidos.Interfaces;
using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Produtos.Interfaces;
using FoodChallenge.Order.Adapter.Gateways;
using FoodChallenge.Order.Application.Pedidos.Models.Requests;
using FoodChallenge.Order.Application.Pedidos.Models.Responses;
using FoodChallenge.Order.Application.Pedidos.UseCases;

namespace FoodChallenge.Order.Adapter.Controllers;

public class PedidoAppController(ValidationContext validationContext,
    IUnitOfWork unitOfWork,
    IClienteRepository clienteDataSource,
    IPedidoRepository pedidoDataSource,
    IPedidoPagamentoRepository pagamentoDataSource,
    IProdutoRepository produtoDataSource,
    IProdutoImagemRepository produtoImagemDataSource,
    IMercadoPagoClient mercadoPagoClient,
    MercadoPagoSettings mercadoPagoSettings)
{
    public async Task<Resposta> ObterPedidoAsync(Guid idPedido, CancellationToken cancellationToken)
    {
        var pedidoGateway = new PedidoGateway(pedidoDataSource);
        var useCase = new ObtemPedidoUseCase(pedidoGateway);

        var pedido = await useCase.ExecutarAsync(idPedido, cancellationToken);
        return Resposta<PedidoResponse>.ComSucesso(PedidoPresenter.ToResponse(pedido));
    }

    public async Task<Resposta> CadastrarAsync(CadastrarPedidoRequest request, CancellationToken cancellationToken)
    {
        var clienteGateway = new ClienteGateway(clienteDataSource);
        var pedidoGateway = new PedidoGateway(pedidoDataSource);
        var pagamentoGateway = new PagamentoGateway(pagamentoDataSource, mercadoPagoClient, mercadoPagoSettings);
        var produtoGateway = new ProdutoGateway(produtoDataSource, produtoImagemDataSource);
        var useCase = new CadastraPedidoUseCase(validationContext, unitOfWork, clienteGateway, pedidoGateway, pagamentoGateway, produtoGateway);

        var pedidoItens = request?.Itens?.Select(PedidoItemMapper.ToDomain);
        var pedidoRetorno = await useCase.ExecutarAsync(request?.Cpf, pedidoItens, cancellationToken);
        return Resposta<PedidoResponse>.ComSucesso(PedidoPresenter.ToResponse(pedidoRetorno));
    }

    public async Task<Resposta> FinalizarPedidoAsync(Guid idPedido, CancellationToken cancellationToken)
    {
        var pedidoGateway = new PedidoGateway(pedidoDataSource);
        var useCase = new FinalizaPedidoUseCase(validationContext, unitOfWork, pedidoGateway);

        var pedido = await useCase.ExecutarAsync(idPedido, cancellationToken);
        return Resposta<PedidoResponse>.ComSucesso(PedidoPresenter.ToResponse(pedido));
    }
}
