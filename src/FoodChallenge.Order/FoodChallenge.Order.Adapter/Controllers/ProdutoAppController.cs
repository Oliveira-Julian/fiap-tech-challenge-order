using FoodChallenge.Common.Entities;
using FoodChallenge.Common.Interfaces;
using FoodChallenge.Common.Validators;
using FoodChallenge.CrossCutting.Paging;
using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Pedidos.Interfaces;
using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Produtos.Interfaces;
using FoodChallenge.Order.Adapter.Gateways;
using FoodChallenge.Order.Adapter.Mappers;
using FoodChallenge.Order.Adapter.Presenters;
using FoodChallenge.Order.Application.Produtos.Imagem.Models.Requests;
using FoodChallenge.Order.Application.Produtos.Imagem.UseCases;
using FoodChallenge.Order.Application.Produtos.Mappers;
using FoodChallenge.Order.Application.Produtos.Models.Reponses;
using FoodChallenge.Order.Application.Produtos.Models.Requests;
using FoodChallenge.Order.Application.Produtos.UseCases;
using FoodChallenge.Order.Domain.Globalization;
using FoodChallenge.Order.Domain.Produtos;

namespace FoodChallenge.Order.Adapter.Controllers;

public class ProdutoAppController(ValidationContext validationContext,
    IUnitOfWork unitOfWork,
    IProdutoRepository produtoDataSource,
    IProdutoImagemRepository produtoImagemDataSource,
    IPedidoRepository pedidoDataSource)
{
    public async Task<Resposta> ObterProdutoAsync(Guid idProduto, CancellationToken cancellationToken)
    {
        var produtoGateway = new ProdutoGateway(produtoDataSource, produtoImagemDataSource);
        var useCase = new ObtemProdutoUseCase(produtoGateway);

        var produto = await useCase.ExecutarAsync(idProduto, cancellationToken);
        return Resposta<ProdutoResponse>.ComSucesso(ProdutoPresenter.ToResponse(produto));
    }

    public async Task<Resposta> ObterPorFiltroAsync(FiltrarProdutoRequest request, CancellationToken cancellationToken)
    {
        var produtoGateway = new ProdutoGateway(produtoDataSource, produtoImagemDataSource);
        var useCase = new PesquisaProdutoUseCase(produtoGateway);

        var filtro = ProdutoMapper.ToDomain(request);
        var produtosPaginacao = await useCase.ExecutarAsync(filtro, cancellationToken);
        return Resposta<Pagination<ProdutoResponse>>.ComSucesso(ProdutoPresenter.ToPaginationResponse(produtosPaginacao));
    }

    public async Task<Resposta> CadastrarProdutoAsync(IEnumerable<ProdutoRequest> request, CancellationToken cancellationToken)
    {
        var produtoGateway = new ProdutoGateway(produtoDataSource, produtoImagemDataSource);
        var useCase = new CadastraProdutosUseCase(validationContext, unitOfWork, produtoGateway);

        var produtos = request?.Select(p => ProdutoMapper.ToDomain(p, null));
        var produtosRetorno = await useCase.ExecutarAsync(produtos, cancellationToken);
        return Resposta<IEnumerable<ProdutoResponse>>.ComSucesso(produtosRetorno?.Select(ProdutoPresenter.ToResponse));
    }

    public async Task<Resposta> AtualizarProdutoAsync(Guid idProduto, ProdutoRequest request, CancellationToken cancellationToken)
    {
        var produtoGateway = new ProdutoGateway(produtoDataSource, produtoImagemDataSource);
        var useCase = new AtualizaProdutoUseCase(validationContext, unitOfWork, produtoGateway);

        var produto = ProdutoMapper.ToDomain(request, idProduto);
        var produtoRetorno = await useCase.ExecutarAsync(produto, cancellationToken);
        return Resposta<ProdutoResponse>.ComSucesso(ProdutoPresenter.ToResponse(produtoRetorno));
    }

    public async Task<Resposta> RemoverProdutoAsync(Guid idProduto, CancellationToken cancellationToken)
    {
        var produtoGateway = new ProdutoGateway(produtoDataSource, produtoImagemDataSource);
        var pedidoGateway = new PedidoGateway(pedidoDataSource);
        var useCase = new RemoveProdutoUseCase(validationContext, unitOfWork, produtoGateway, pedidoGateway);

        await useCase.ExecutarAsync(idProduto, cancellationToken);
        return Resposta.ComSucesso(string.Format(Textos.RemovidoComSucesso, nameof(Produto)));
    }

    public async Task<Resposta> CadastrarProdutoImagensAsync(Guid idProduto, ProdutoImagemRequest request, CancellationToken cancellationToken)
    {
        var produtoGateway = new ProdutoGateway(produtoDataSource, produtoImagemDataSource);
        var pedidoGateway = new PedidoGateway(pedidoDataSource);
        var useCase = new CadastraProdutoImagemUseCase(validationContext, unitOfWork, produtoGateway);

        var produtoImagens = ProdutoImagemMapper.ToDomain(idProduto, request);
        await useCase.ExecutarAsync(idProduto, produtoImagens, cancellationToken);
        return Resposta.ComSucesso(string.Format(Textos.ProdutoImagensCadastradas, idProduto));
    }

    public async Task<Resposta> RemoverProdutoImagemAsync(Guid idProduto, Guid idImagem, CancellationToken cancellationToken)
    {
        var produtoGateway = new ProdutoGateway(produtoDataSource, produtoImagemDataSource);
        var pedidoGateway = new PedidoGateway(pedidoDataSource);
        var useCase = new RemoveProdutoImagemUseCase(validationContext, unitOfWork, produtoGateway);

        await useCase.ExecutarAsync(idProduto, idImagem, cancellationToken);
        return Resposta.ComSucesso(string.Format(Textos.RemovidoComSucesso, nameof(ProdutoImagem)));
    }
}
