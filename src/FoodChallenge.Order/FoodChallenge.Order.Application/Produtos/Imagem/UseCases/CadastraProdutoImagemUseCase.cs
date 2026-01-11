using FoodChallenge.Common.Interfaces;
using FoodChallenge.Common.Validators;
using FoodChallenge.Order.Application.Produtos.Imagem.Specifications;
using FoodChallenge.Order.Application.Produtos.Interfaces;
using FoodChallenge.Order.Domain.Constants;
using FoodChallenge.Order.Domain.Globalization;
using FoodChallenge.Order.Domain.Produtos;
using Serilog;

namespace FoodChallenge.Order.Application.Produtos.Imagem.UseCases;

public sealed class CadastraProdutoImagemUseCase(
    ValidationContext validationContext,
    IUnitOfWork unitOfWork,
    IProdutoGateway produtoGateway) : ICadastraProdutoImagemUseCase
{
    private readonly ILogger logger = Log.ForContext<CadastraProdutoImagemUseCase>();

    public async Task ExecutarAsync(Guid idProduto, IEnumerable<ProdutoImagem> produtoImagens, CancellationToken cancellationToken)
    {
        logger.Information(Logs.InicioExecucaoServico, nameof(CadastraProdutoImagemUseCase), nameof(ExecutarAsync));

        try
        {
            var produto = await produtoGateway.ObterProdutoPorIdAsync(idProduto, cancellationToken);

            if (!(produto?.Ativo ?? false))
            {
                validationContext.AddValidation(string.Format(Textos.NaoEncontrado, nameof(Produto)));
                return;
            }

            if (!(produtoImagens?.Any()) ?? true)
            {
                validationContext.AddValidation(string.Format(Textos.ItemObrigatorio, "Imagem"));
                return;
            }

            var specification = new ProdutoImagemSpecification();

            foreach (var imagem in produtoImagens)
            {
                validationContext.AddValidations(imagem, specification);
            }

            if (validationContext.HasValidations)
                return;

            unitOfWork.BeginTransaction();
            await produtoGateway.CadastrarImagensAsync(produtoImagens, cancellationToken);
            await unitOfWork.CommitAsync();

            logger.Information(Logs.FimExecucaoServico, nameof(CadastraProdutoImagemUseCase), nameof(ExecutarAsync),
                $"Imagens cadastradas para o produto, IdProduto: {produtoImagens.Select(pi => pi.IdProduto).FirstOrDefault()}");
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackAsync();
            logger.Error(ex, Logs.ErroGenerico, nameof(CadastraProdutoImagemUseCase), nameof(ExecutarAsync));
            throw;
        }
    }
}
