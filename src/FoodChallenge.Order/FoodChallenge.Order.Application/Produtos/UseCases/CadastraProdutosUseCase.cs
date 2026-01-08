using FoodChallenge.Common.Interfaces;
using FoodChallenge.Common.Validators;
using FoodChallenge.Order.Application.Preparos;
using FoodChallenge.Order.Application.Produtos.Interfaces;
using FoodChallenge.Order.Application.Produtos.Specifications;
using FoodChallenge.Order.Domain.Constants;
using FoodChallenge.Order.Domain.Produtos;
using Serilog;

namespace FoodChallenge.Order.Application.Produtos.UseCases;

public sealed class CadastraProdutosUseCase(
    ValidationContext validationContext,
    IUnitOfWork unitOfWork,
    IProdutoGateway produtoGateway) : ICadastraProdutosUseCase
{
    private readonly ILogger logger = Log.ForContext<CadastraProdutosUseCase>();

    public async Task<IEnumerable<Produto>> ExecutarAsync(IEnumerable<Produto> produtos, CancellationToken cancellationToken)
    {
        logger.Information(Logs.InicioExecucaoServico, nameof(CadastraProdutosUseCase), nameof(ExecutarAsync));

        try
        {
            foreach (var produto in produtos)
            {
                validationContext.AddValidations(produto, new ProdutoCadastroSpecification());
            }

            if (validationContext.HasValidations)
                return default;

            unitOfWork.BeginTransaction();
            var produtoCadastrados = produtos as Produto[] ?? produtos.ToArray();
            await produtoGateway.CadastrarProdutosAsync(produtoCadastrados, cancellationToken);
            await unitOfWork.CommitAsync();

            logger.Information(Logs.FimExecucaoServico, nameof(CadastraProdutosUseCase), nameof(ExecutarAsync), produtoCadastrados);

            return produtoCadastrados;

        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackAsync();
            logger.Error(ex, Logs.ErroGenerico, nameof(CadastraProdutosUseCase), nameof(ExecutarAsync));
            throw;
        }
    }
}
