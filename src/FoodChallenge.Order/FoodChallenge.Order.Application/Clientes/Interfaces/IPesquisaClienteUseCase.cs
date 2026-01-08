using FoodChallenge.CrossCutting.Paging;
using FoodChallenge.Order.Domain.Clientes;

namespace FoodChallenge.Order.Application.Clientes.Interfaces;

public interface IPesquisaClienteUseCase
{
    Task<Pagination<Cliente>> ExecutarAsync(Filter<ClienteFilter> filtro, CancellationToken cancellationToken);
}
