using FoodChallenge.Common.Interfaces;
using FoodChallenge.Common.Validators;
using FoodChallenge.Order.Application.Produtos.Interfaces;
using FoodChallenge.Order.Domain.Constants;
using FoodChallenge.Order.Domain.Globalization;
using FoodChallenge.Order.Domain.Produtos;
using Serilog;

namespace FoodChallenge.Order.Application.Produtos.Imagem.UseCases;

public sealed class RemoveProdutoImagemUseCase(
    ValidationContext validationContext,
    IUnitOfWork unitOfWork,
    IProdutoGateway produtoGateway) : IRemoveProdutoImagemUseCase
{
    private readonly ILogger logger = Log.ForContext<RemoveProdutoImagemUseCase>();

    public async Task ExecutarAsync(Guid idProduto, Guid idImagem, CancellationToken cancellationToken)
    {
        logger.Information(Logs.InicioExecucaoServico, nameof(RemoveProdutoImagemUseCase), nameof(ExecutarAsync));

        try
        {
            var produto = await produtoGateway.ObterProdutoPorIdAsync(idProduto, cancellationToken);
            if (produto is null)
            {
                validationContext.AddValidation(string.Format(Textos.NaoEncontrado, nameof(Produto)));
                return;
            }

            var produtoImagem = await produtoGateway.ObterProdutoImagemPorIdAsync(idImagem, cancellationToken);
            if (produtoImagem is null)
            {
                validationContext.AddValidation(string.Format(Textos.NaoEncontrado, "Imagem"));
                return;
            }

            unitOfWork.BeginTransaction();
            produtoGateway.RemoverProdutoImagem(produtoImagem);
            await unitOfWork.CommitAsync();

            logger.Information(Logs.FimExecucaoServico, nameof(RemoveProdutoImagemUseCase), nameof(ExecutarAsync), idImagem);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackAsync();
            logger.Error(ex, Logs.ErroGenerico, nameof(RemoveProdutoImagemUseCase), nameof(ExecutarAsync));
            throw;
        }
    }
}
