using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Bases;

namespace FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Clientes.Interfaces;

public interface IClienteRepository : IRepositoryBase<ClienteEntity>
{
    Task<ClienteEntity> ObterPorCpfAsync(
        string cpf,
        CancellationToken cancellationToken,
        bool tracking = false);
}
