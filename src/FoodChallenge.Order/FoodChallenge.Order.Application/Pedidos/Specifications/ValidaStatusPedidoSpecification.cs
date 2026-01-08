using FluentValidation;
using FoodChallenge.Common.Validators;
using FoodChallenge.Order.Domain.Enums;
using FoodChallenge.Order.Domain.Globalization;
using FoodChallenge.Order.Domain.Pedidos;

namespace FoodChallenge.Order.Application.Pedidos.Specifications;

public sealed class ValidaStatusPedidoEnumSpecification : FluentValidatorBase<PedidoStatus>
{
    public ValidaStatusPedidoEnumSpecification()
    {
        RuleFor(status => status)
            .Must(status => Enum.IsDefined(typeof(PedidoStatus), status))
            .WithMessage(status => string.Format(Textos.CampoInvalido, nameof(Pedido.Status)));
    }
}
