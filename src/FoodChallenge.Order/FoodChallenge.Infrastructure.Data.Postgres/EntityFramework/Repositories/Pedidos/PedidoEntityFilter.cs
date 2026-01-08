using FoodChallenge.CrossCutting.Paging;

namespace FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Pedidos;

public class PedidoEntityFilter
{
    [FilterBy(nameof(PedidoEntity.Id), FilterType.Equals)]
    public Guid? Id { get; set; }

    [FilterBy(nameof(PedidoEntity.Cliente.Id), FilterType.Equals)]
    public Guid? IdCliente { get; set; }

    [FilterBy(nameof(PedidoEntity.Ativo), FilterType.Equals)]
    public bool Ativo { get; set; } = true;
}
