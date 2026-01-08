using FoodChallenge.Common.Extensions;
using FoodChallenge.CrossCutting.Paging;
using FoodChallenge.Order.Adapter.Presenters;
using FoodChallenge.Order.Application.Pedidos.Models.Responses;
using FoodChallenge.Order.Domain.Pedidos;

namespace FoodChallenge.Order.Adapter.Presenters;

public static class PedidoPresenter
{
    public static PedidoResponse ToResponse(Pedido pedido)
    {
        if (pedido is null) return default;

        return new PedidoResponse()
        {
            Id = pedido.Id,
            Codigo = pedido.Codigo,
            Cliente = ClientePresenter.ToResponse(pedido.Cliente),
            Itens = pedido.Itens?.Select(PedidoItemPresenter.ToResponse),
            ValorTotal = pedido.ValorTotal,
            Status = pedido.Status,
            DescricaoStatus = pedido.Status.GetDescription(),
            Pagamento = PagamentoPresenter.ToResponse(pedido.Pagamento)
        };
    }

    public static Pagination<PedidoResponse> ToPaginationResponse(Pagination<Pedido> pagination)
    {
        if (pagination is null) return default;

        return new Pagination<PedidoResponse>(
            pagination.Page,
            pagination.SizePage,
            pagination.TotalRecords,
            pagination.Records?.Select(ToResponse));
    }
}
