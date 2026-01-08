using FoodChallenge.CrossCutting.Paging;
using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Pedidos;

namespace FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Preparos;

public class OrdemPedidoEntityFilter
{
    [FilterBy(nameof(OrdemPedidoEntity.Status))]
    public IEnumerable<int> Status { get; set; }

    [FilterBy(nameof(PedidoEntity.Status), FilterType.NotEqual)]
    public int? StatusPedidoDiferente { get; set; }
}
