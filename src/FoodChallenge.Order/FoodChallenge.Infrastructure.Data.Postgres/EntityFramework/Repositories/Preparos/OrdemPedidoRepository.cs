using FoodChallenge.CrossCutting.Paging;
using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Context;
using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Preparos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Preparos;

public class OrdemPedidoRepository(EntityFrameworkContext context)
    : RepositoryBase<OrdemPedidoEntity>(context), IOrdemPedidoRepository
{
    public async Task<OrdemPedidoEntity> GetByIdWithRelationsAsync(Guid id, CancellationToken cancellationToken, bool tracking = false)
    {
        return await GetQuery(tracking)
            .Include(o => o.Pedido)
            .ThenInclude(o => o.Itens)
            .FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
    }

    public async Task<Pagination<OrdemPedidoEntity>> GetOrdersOrderDefaultAsync(Filter<OrdemPedidoEntityFilter> filter, CancellationToken cancellationToken)
    {
        var query = GetQuery();

        query = ApplyFilters(query, filter.Fields);

        query = query
            .Include(x => x.Pedido)
            .OrderByDescending(x => x.Status)
            .ThenBy(x => x.DataInicioPreparacao)
            .ThenBy(x => x.DataCriacao);

        if (filter.Fields.StatusPedidoDiferente > 0)
        {
            query = query.Where(x => x.Pedido.Status != filter.Fields.StatusPedidoDiferente);
        }

        var total = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((filter.Page - 1) * filter.SizePage)
            .Take(filter.SizePage)
            .ToListAsync(cancellationToken);

        return new Pagination<OrdemPedidoEntity>(filter.Page, items.Count, total, items);
    }
}
