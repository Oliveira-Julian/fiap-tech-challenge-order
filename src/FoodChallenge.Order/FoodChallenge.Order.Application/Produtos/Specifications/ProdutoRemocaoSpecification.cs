using FluentValidation;
using FoodChallenge.Common.Validators;
using FoodChallenge.Order.Application.Pedidos;
using FoodChallenge.Order.Domain.Globalization;
using FoodChallenge.Order.Domain.Pedidos;
using FoodChallenge.Order.Domain.Produtos;

namespace FoodChallenge.Order.Application.Produtos.Specifications;

public sealed class ProdutoRemocaoSpecification : FluentValidatorBase<Produto>
{
    public ProdutoRemocaoSpecification(IPedidoGateway pedidoGateway)
    {
        RuleFor(produto => produto.Id.Value)
            .MustAsync(async (id, cancellation) =>
            {
                var pedidos = await pedidoGateway.ObterPedidosPorProdutoAsync(id, Pedido.ObterTodosStatusNaoFinalizados(), cancellation);
                return pedidos == null || !pedidos.Any();
            })
            .WithMessage(Textos.ProdutoRemocaoNaoPermitida);
    }
}

