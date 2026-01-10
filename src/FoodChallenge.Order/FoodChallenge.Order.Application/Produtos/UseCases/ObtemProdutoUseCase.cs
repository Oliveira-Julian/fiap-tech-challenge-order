using FoodChallenge.Order.Application.Produtos.Interfaces;
using FoodChallenge.Order.Domain.Constants;
using FoodChallenge.Order.Domain.Produtos;
using Serilog;

namespace FoodChallenge.Order.Application.Produtos.UseCases;

public sealed class ObtemProdutoUseCase(IProdutoGateway produtoGateway) : IObtemProdutoUseCase
{
    private readonly ILogger logger = Log.ForContext<ObtemProdutoUseCase>();

    public async Task<Produto> ExecutarAsync(Guid id, CancellationToken cancellationToken)
    {
        logger.Information(Logs.InicioExecucaoServico, nameof(ObtemProdutoUseCase), nameof(ExecutarAsync));

        try
        {
            var produto = await produtoGateway.ObterProdutoPorIdComImagensAsync(id, cancellationToken);

            logger.Information(Logs.FimExecucaoServico, nameof(ObtemProdutoUseCase), nameof(ExecutarAsync), produto);

            return produto;
        }
        catch (Exception ex)
        {
            logger.Error(ex, Logs.ErroGenerico, nameof(ObtemProdutoUseCase), nameof(ExecutarAsync));
            throw;
        }
    }
}
