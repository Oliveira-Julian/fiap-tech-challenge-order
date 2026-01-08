using FoodChallenge.CrossCutting.Paging;
using FoodChallenge.Order.Application.Clientes.Interfaces;
using FoodChallenge.Order.Domain.Clientes;
using FoodChallenge.Order.Domain.Constants;
using Serilog;

namespace FoodChallenge.Order.Application.Clientes.UseCases;

public sealed class PesquisaClienteUseCase(
    IClienteGateway gateway) : IPesquisaClienteUseCase
{
    private readonly ILogger logger = Log.ForContext<PesquisaClienteUseCase>();

    public async Task<Pagination<Cliente>> ExecutarAsync(Filter<ClienteFilter> filtro, CancellationToken cancellationToken)
    {
        logger.Information(Logs.InicioExecucaoServico, nameof(PesquisaClienteUseCase), nameof(ExecutarAsync));

        try
        {
            var clientesPaginacao = await gateway.ObterClienteAsync(filtro, cancellationToken);

            logger.Information(Logs.FimExecucaoServico, nameof(PesquisaClienteUseCase), nameof(ExecutarAsync), clientesPaginacao);

            return clientesPaginacao;
        }
        catch (Exception ex)
        {
            logger.Error(ex, Logs.ErroGenerico, nameof(PesquisaClienteUseCase), nameof(ExecutarAsync));
            throw;
        }
    }
}
