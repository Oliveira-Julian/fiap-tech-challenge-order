using FoodChallenge.Order.Domain.Entities;
using FoodChallenge.Order.Domain.Enums;
using FoodChallenge.Order.Domain.Pedidos;

namespace FoodChallenge.Order.Domain.Preparos;

public class OrdemPedido : DomainBase
{
    public Guid IdPedido { get; set; }
    public Pedido Pedido { get; set; }
    public PreparoStatus Status { get; set; }

    public OrdemPedido(Pedido pedido)
    {
        if (pedido is null)
            return;

        IdPedido = pedido.Id.Value;
        Pedido = pedido;
        Status = PreparoStatus.NaFilaParaPreparacao;
    }
}
