using FoodChallenge.Infrastructure.Clients.Payments.Models;
using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Pagamentos;
using FoodChallenge.Order.Domain.Pedidos;
using FoodChallenge.Payment.Domain.Pagamentos;

namespace FoodChallenge.Order.Adapter.Mappers;

public static class PagamentoMapper
{
    public static CriarPagamentoRequest ToRequest(Pedido pedido)
    {
        if (pedido is null) return default;

        return new CriarPagamentoRequest
        {
            IdPedido = pedido.Id,
            CodigoPedido = pedido.Codigo,
            ValorTotal = pedido.ValorTotal,
            Itens = pedido.Itens?.Select(ToRequest)
        };
    }

    public static CriarPagamentoItemRequest ToRequest(PedidoItem pedidoItem)
    {
        if (pedidoItem is null) return default;

        return new CriarPagamentoItemRequest
        {
            Categoria = pedidoItem.Produto.Categoria.ToString(),
            Codigo = pedidoItem.Codigo,
            Nome = pedidoItem.Produto.Nome,
            Quantidade = pedidoItem.Quantidade,
            Valor = pedidoItem.Valor
        };
    }

    public static Pagamento ToDomain(PagamentoResponse pagamentoResponse)
    {
        if (pagamentoResponse is null) return default;

        return new Pagamento(pagamentoResponse.Id, pagamentoResponse.Status, pagamentoResponse.QrCode);
    }

    public static PagamentoEntity ToEntity(Guid idPedido, Pagamento pagamento)
    {
        if (pagamento is null) return default;

        return new PagamentoEntity
        {
            Id = pagamento.Id,
            IdPedido = idPedido,
            DataCriacao = pagamento.DataCriacao,
            Ativo = pagamento.Ativo,
            QrCode = pagamento.QrCode,
            Status = (int)pagamento.Status,
        };
    }
}
