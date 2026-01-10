using FoodChallenge.Common.Entities;
using FoodChallenge.Common.Interfaces;
using FoodChallenge.Common.Validators;
using FoodChallenge.CrossCutting.Paging;
using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Clientes.Interfaces;
using FoodChallenge.Order.Adapter.Gateways;
using FoodChallenge.Order.Adapter.Mappers;
using FoodChallenge.Order.Adapter.Presenters;
using FoodChallenge.Order.Application.Clientes.Models.Requests;
using FoodChallenge.Order.Application.Clientes.Models.Responses;
using FoodChallenge.Order.Application.Clientes.UseCases;

namespace FoodChallenge.Order.Adapter.Controllers;

public class ClienteAppController(ValidationContext validationContext,
    IUnitOfWork unitOfWork,
    IClienteRepository clienteDataSource)
{
    public async Task<Resposta<Pagination<ClienteResponse>>> PesquisarClienteAsync(FiltrarClienteRequest request, CancellationToken cancellationToken)
    {
        var clienteGateway = new ClienteGateway(clienteDataSource);
        var useCase = new PesquisaClienteUseCase(clienteGateway);

        var filtro = ClienteMapper.ToDomain(request);
        var cliente = await useCase.ExecutarAsync(filtro, cancellationToken);
        return Resposta<Pagination<ClienteResponse>>.ComSucesso(ClientePresenter.ToPaginationResponse(cliente));
    }

    public async Task<Resposta> RegistrarAsync(RegistrarClienteRequest request, CancellationToken cancellationToken)
    {
        var clienteGateway = new ClienteGateway(clienteDataSource);
        var useCase = new RegistraClienteUseCase(validationContext, unitOfWork, clienteGateway);

        var cliente = ClienteMapper.ToDomain(request);
        var clienteRetorno = await useCase.ExecutarAsync(cliente, cancellationToken);
        return Resposta<ClienteResponse>.ComSucesso(ClientePresenter.ToResponse(clienteRetorno));
    }
}
