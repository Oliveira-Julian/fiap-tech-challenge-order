using FoodChallenge.Common.Interfaces;
using FoodChallenge.Common.Validators;
using FoodChallenge.Order.Application.Produtos.Interfaces;
using FoodChallenge.Order.Application.Produtos.Specifications;
using FoodChallenge.Order.Domain.Constants;
using FoodChallenge.Order.Domain.Globalization;
using FoodChallenge.Order.Domain.Produtos;
using Serilog;

namespace FoodChallenge.Order.Application.Produtos.UseCases;

public sealed class AtualizaProdutoUseCase(
    ValidationContext validationContext,
    IUnitOfWork unitOfWork,
    IProdutoGateway produtoGateway) : IAtualizaProdutoUseCase
{
    private readonly ILogger logger = Log.ForContext<AtualizaProdutoUseCase>();

    public async Task<Produto> ExecutarAsync(Produto produto, CancellationToken cancellationToken)
    {
        logger.Information(Logs.InicioExecucaoServico, nameof(AtualizaProdutoUseCase), nameof(ExecutarAsync));

        try
        {
            validationContext.AddValidations(produto, new ProdutoAtualizacaoSpecification());

            if (validationContext.HasValidations)
                return default;

            var produtoEditado = await produtoGateway.ObterProdutoPorIdAsync(produto.Id.Value, cancellationToken);
            if (produtoEditado is null)
            {
                validationContext.AddValidation(string.Format(Textos.NaoEncontrado, nameof(Produto)));
                return default;
            }

            produto.Atualizar();

            unitOfWork.BeginTransaction();
            produtoGateway.AtualizarProduto(produto);
            await unitOfWork.CommitAsync();

            produtoEditado = await produtoGateway.ObterProdutoPorIdAsync(produto.Id.Value, cancellationToken);

            if (validationContext.HasValidations)
                return default;

            logger.Information(Logs.FimExecucaoServico, nameof(AtualizaProdutoUseCase), nameof(ExecutarAsync), produtoEditado);

            return produtoEditado;
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackAsync();
            logger.Error(ex, Logs.ErroGenerico, nameof(AtualizaProdutoUseCase), nameof(ExecutarAsync));
            throw;
        }
    }
}
