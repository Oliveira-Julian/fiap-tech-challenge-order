using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Context;
using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Clientes.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Clientes;

public class ClienteRepository(EntityFrameworkContext context)
    : RepositoryBase<ClienteEntity>(context), IClienteRepository
{
    public async Task<ClienteEntity> ObterPorCpfAsync(
        string cpf,
        CancellationToken cancellationToken,
        bool tracking = false)
    {
        return await GetQuery(tracking)
            .Where(c => c.Cpf == cpf)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
