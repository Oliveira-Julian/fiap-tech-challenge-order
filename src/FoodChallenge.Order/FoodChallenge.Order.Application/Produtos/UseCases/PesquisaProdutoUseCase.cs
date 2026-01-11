using FoodChallenge.CrossCutting.Paging;
using FoodChallenge.Order.Application.Produtos.Interfaces;
using FoodChallenge.Order.Domain.Constants;
using FoodChallenge.Order.Domain.Produtos;
using Serilog;

namespace FoodChallenge.Order.Application.Produtos.UseCases;

public sealed class PesquisaProdutoUseCase(IProdutoGateway produtoGateway) : IPesquisaProdutoUseCase
{
    private readonly ILogger logger = Log.ForContext<PesquisaProdutoUseCase>();

    public async Task<Pagination<Produto>> ExecutarAsync(Filter<ProdutoFilter> filtro, CancellationToken cancellationToken)
    {
        logger.Information(Logs.InicioExecucaoServico, nameof(PesquisaProdutoUseCase), nameof(ExecutarAsync));

        try
        {
            var produtosPaginacao = await produtoGateway.ObterProdutosPorFiltroAsync(filtro, cancellationToken);

            logger.Information(Logs.FimExecucaoServico, nameof(PesquisaProdutoUseCase), nameof(ExecutarAsync), produtosPaginacao);

            return produtosPaginacao;
        }
        catch (Exception ex)
        {
            logger.Error(ex, Logs.ErroGenerico, nameof(PesquisaProdutoUseCase), nameof(ExecutarAsync));
            throw;
        }
    }
}
