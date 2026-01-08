using FoodChallenge.CrossCutting.Paging;
using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Clientes;
using FoodChallenge.Order.Application.Clientes.Models.Requests;
using FoodChallenge.Order.Domain.Clientes;
using FoodChallenge.Order.Domain.Clientes.ValueObjects;

namespace FoodChallenge.Order.Adapter.Mappers;

public static class ClienteMapper
{
    public static Cliente ToDomain(ClienteEntity clienteEntity)
    {
        if (clienteEntity is null) return default;

        return new Cliente()
        {
            Id = clienteEntity.Id,
            Ativo = clienteEntity.Ativo,
            DataAtualizacao = clienteEntity.DataAtualizacao,
            DataCriacao = clienteEntity.DataCriacao,
            DataExclusao = clienteEntity.DataExclusao,
            Cpf = new Cpf(clienteEntity.Cpf),
            Email = new Email(clienteEntity.Email),
            Nome = clienteEntity.Nome,
            Pedidos = clienteEntity.Pedidos?.Select(PedidoMapper.ToDomain)
        };
    }

    public static ClienteEntity ToEntity(Cliente cliente)
    {
        if (cliente is null) return default;

        return new ClienteEntity()
        {
            Id = cliente.Id,
            Ativo = cliente.Ativo,
            DataAtualizacao = cliente.DataAtualizacao,
            DataCriacao = cliente.DataCriacao,
            DataExclusao = cliente.DataExclusao,
            Cpf = cliente.Cpf?.ToString(),
            Email = cliente.Email?.Valor,
            Nome = cliente.Nome,
            Pedidos = cliente.Pedidos?.Select(PedidoMapper.ToEntity)?.ToList()
        };
    }

    public static Cliente ToDomain(RegistrarClienteRequest registrarClienteRequest)
    {
        if (registrarClienteRequest is null) return default;

        return new Cliente()
        {
            Cpf = new Cpf(registrarClienteRequest.Cpf),
            Email = new Email(registrarClienteRequest.Email),
            Nome = registrarClienteRequest.Nome
        };
    }

    public static Filter<ClienteFilter> ToDomain(FiltrarClienteRequest filterRequest)
    {
        var clienteFilter = new ClienteFilter()
        {
            Cpf = filterRequest?.Cpf,
            Ativo = true
        };

        if (filterRequest is null)
            return new Filter<ClienteFilter>(1, 30, clienteFilter);

        return new Filter<ClienteFilter>(filterRequest.Page,
            filterRequest.SizePage,
            filterRequest.FieldOrdenation,
            filterRequest.OrdenationAsc,
            clienteFilter);
    }

    public static Pagination<Cliente> ToPagination(Pagination<ClienteEntity> pagination)
    {
        if (pagination is null) return default;

        return new Pagination<Cliente>(
            pagination.Page,
            pagination.SizePage,
            pagination.TotalRecords,
            pagination.Records?.Select(ToDomain));
    }

    public static Filter<ClienteEntityFilter> ToEntityFilter(Filter<ClienteFilter> domainFilter)
    {
        if (domainFilter == null) return null;

        var filter = new ClienteEntityFilter
        {
            Cpf = domainFilter.Fields?.Cpf,
            Ativo = domainFilter.Fields?.Ativo ?? true
        };

        return new Filter<ClienteEntityFilter>(
            domainFilter.Page,
            domainFilter.SizePage,
            domainFilter.FieldOrdenation,
            domainFilter.OrdenationAsc,
            filter
        );
    }
}
