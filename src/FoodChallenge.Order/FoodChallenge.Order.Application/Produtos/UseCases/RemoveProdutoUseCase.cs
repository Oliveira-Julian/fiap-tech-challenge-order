using FoodChallenge.Common.Interfaces;
using FoodChallenge.Common.Validators;
using FoodChallenge.Order.Application.Pedidos;
using FoodChallenge.Order.Application.Preparos;
using FoodChallenge.Order.Application.Produtos.Interfaces;
using FoodChallenge.Order.Application.Produtos.Specifications;
using FoodChallenge.Order.Domain.Constants;
using FoodChallenge.Order.Domain.Globalization;
using FoodChallenge.Order.Domain.Produtos;
using Serilog;

namespace FoodChallenge.Order.Application.Produtos.UseCases;

public sealed class RemoveProdutoUseCase(
    ValidationContext validationContext,
    IUnitOfWork unitOfWork,
    IProdutoGateway produtoGateway,
    IPedidoGateway pedidoGateway) : IRemoveProdutoUseCase
{
    private readonly ILogger logger = Log.ForContext<CadastraProdutosUseCase>();

    public async Task ExecutarAsync(Guid id, CancellationToken cancellationToken)
    {
        logger.Information(Logs.InicioExecucaoServico, nameof(RemoveProdutoUseCase), nameof(ExecutarAsync));

        try
        {
            var produto = await produtoGateway.ObterProdutoPorIdAsync(id, cancellationToken);
            if (produto is null)
            {
                validationContext.AddValidation(string.Format(Textos.NaoEncontrado, nameof(Produto)));
                return;
            }

            await validationContext.AddValidationsAsync(produto, cancellationToken, new ProdutoRemocaoSpecification(pedidoGateway));
            if (validationContext.HasValidations)
                return;


            produto.Excluir();

            unitOfWork.BeginTransaction();
            produtoGateway.AtualizarProduto(produto);
            await unitOfWork.CommitAsync();

            logger.Information(Logs.FimExecucaoServico, nameof(RemoveProdutoUseCase), nameof(ExecutarAsync), id);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackAsync();
            logger.Error(ex, Logs.ErroGenerico, nameof(RemoveProdutoUseCase), nameof(ExecutarAsync));
            throw;
        }
    }
}
