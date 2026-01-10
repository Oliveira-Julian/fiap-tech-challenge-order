using FluentValidation;
using FoodChallenge.Common.Validators;
using FoodChallenge.Order.Domain.Enums;
using FoodChallenge.Order.Domain.Globalization;
using FoodChallenge.Order.Domain.Pedidos;

namespace FoodChallenge.Order.Application.Pedidos.Specifications;

public sealed class AtualizaStatusPedidoSpecification : FluentValidatorBase<Pedido>
{
    public AtualizaStatusPedidoSpecification(PedidoStatus status)
    {
        RuleFor(pedido => status)
            .SetValidator(new ValidaStatusPedidoEnumSpecification())
            .DependentRules(() =>
            {
                RuleFor(pedido => pedido)
                    .Custom((pedido, context) =>
                    {
                        if (!pedido.PermitirAlterarStatus(status))
                        {
                            context.AddFailure(string.Format(Textos.PedidoStatusNaoPermitido, pedido.Status, status));
                        }
                    });
            });
    }
}
