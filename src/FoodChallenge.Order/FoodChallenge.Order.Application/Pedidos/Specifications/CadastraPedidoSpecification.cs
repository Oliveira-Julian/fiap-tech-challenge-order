using FluentValidation;
using FoodChallenge.Common.Validators;
using FoodChallenge.Order.Domain.Globalization;
using FoodChallenge.Order.Domain.Pedidos;

namespace FoodChallenge.Order.Application.Pedidos.Specifications;

public sealed class CadastraPedidoSpecification : FluentValidatorBase<Pedido>
{
    public CadastraPedidoSpecification(IEnumerable<Guid> idProdutos)
    {
        RuleFor(pedido => pedido.Status)
            .SetValidator(new ValidaStatusPedidoEnumSpecification());

        RuleFor(p => idProdutos)
            .NotEmpty()
            .WithMessage(string.Format(Textos.CampoObrigatorio, nameof(Pedido.Itens)));

        if (idProdutos is not null && idProdutos.Any())
        {
            RuleFor(pedido => pedido)
                .Must(pedido => pedido.ContemTodosProdutos(idProdutos))
                .WithMessage(pedido =>
                    string.Format(Textos.RegistrosNaoEncontrados,
                                  "produtos",
                                  string.Join(", ", pedido.ObterProdutosFaltando(idProdutos))));
        }
    }
}
