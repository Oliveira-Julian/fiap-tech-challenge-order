using FoodChallenge.CrossCutting.Paging;
using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Clientes.Interfaces;
using FoodChallenge.Order.Adapter.Mappers;
using FoodChallenge.Order.Application.Clientes;
using FoodChallenge.Order.Domain.Clientes;
using FoodChallenge.Order.Domain.Clientes.ValueObjects;

namespace FoodChallenge.Order.Adapter.Gateways;

public class ClienteGateway(IClienteRepository clienteDataSource) : IClienteGateway
{
    public async Task<Pagination<Cliente>> ObterClienteAsync(Filter<ClienteFilter> filtro, CancellationToken cancellationToken)
    {
        var filterEntity = ClienteMapper.ToEntityFilter(filtro);
        var pagedEntity = await clienteDataSource.QueryPagedAsync(filterEntity, cancellationToken);
        return ClienteMapper.ToPagination(pagedEntity);
    }

    public async Task<Cliente> CadastrarClienteAsync(Cliente cliente, CancellationToken cancellationToken)
    {
        var clienteEntity = ClienteMapper.ToEntity(cliente);
        clienteEntity = await clienteDataSource.AddAsync(clienteEntity, cancellationToken);
        return ClienteMapper.ToDomain(clienteEntity);
    }

    public async Task<Cliente> ObterPorCpfAsync(Cpf cpf, CancellationToken cancellationToken)
    {
        var clienteEntity = await clienteDataSource.ObterPorCpfAsync(cpf.ToString(), cancellationToken);
        return ClienteMapper.ToDomain(clienteEntity);
    }

    public async Task<Cliente> ObterClientePadraoAsync(CancellationToken cancellationToken)
    {
        var idClienteNaoIdentificado = Guid.Parse("a0c7ec9b-33ac-43df-85dd-4c38cadb4cda");
        var clienteEntity = await clienteDataSource.GetByIdAsync(idClienteNaoIdentificado, cancellationToken);
        return ClienteMapper.ToDomain(clienteEntity);
    }
}
