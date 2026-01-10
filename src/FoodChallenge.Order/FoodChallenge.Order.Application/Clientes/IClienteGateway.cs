using FoodChallenge.CrossCutting.Paging;
using FoodChallenge.Order.Domain.Clientes;
using FoodChallenge.Order.Domain.Clientes.ValueObjects;

namespace FoodChallenge.Order.Application.Clientes;

public interface IClienteGateway
{
    Task<Pagination<Cliente>> ObterClienteAsync(Filter<ClienteFilter> filtro, CancellationToken cancellationToken);
    Task<Cliente> CadastrarClienteAsync(Cliente cliente, CancellationToken cancellationToken);
    Task<Cliente> ObterPorCpfAsync(Cpf cpf, CancellationToken cancellationToken);
    Task<Cliente> ObterClientePadraoAsync(CancellationToken cancellationToken);
}
