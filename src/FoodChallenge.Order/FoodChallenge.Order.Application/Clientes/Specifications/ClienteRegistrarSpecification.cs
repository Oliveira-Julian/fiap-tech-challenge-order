using FluentValidation;
using FoodChallenge.Common.Validators;
using FoodChallenge.Order.Domain.Clientes;
using FoodChallenge.Order.Domain.Globalization;

namespace FoodChallenge.Order.Application.Clientes.Specifications;

public sealed class ClienteRegistrarSpecification : FluentValidatorBase<Cliente>
{
    public ClienteRegistrarSpecification(IClienteGateway clienteGateway)
    {

        ValidarQuandoCpfPreenchido(clienteGateway);
        ValidarQuandoCpfNaoPreenchido();
    }

    private void ValidarQuandoCpfPreenchido(IClienteGateway clienteGateway)
    {
        When(cliente => cliente.Cpf != null, () =>
        {
            RuleFor(p => p.Cpf)
                .NotEmpty()
                .WithMessage(string.Format(Textos.CampoObrigatorio, nameof(Cliente.Cpf)))
                .Must(cpf => cpf.EhValido())
                .WithMessage(string.Format(Textos.CampoInvalido, nameof(Cliente.Cpf)));

            RuleFor(cliente => cliente)
            .MustAsync(async (cliente, cancellation) =>
            {
                var clienteDb = await clienteGateway.ObterPorCpfAsync(cliente.Cpf, cancellation);
                return clienteDb == null;
            })
            .WithMessage(string.Format(Textos.RegistroJaCadastrado, nameof(Cliente), nameof(Cliente.Cpf)));
        });
    }

    private void ValidarQuandoCpfNaoPreenchido()
    {
        When(cliente => cliente.Cpf is null, () =>
        {
            RuleFor(cliente => cliente.Nome)
                .NotEmpty()
                .WithMessage(string.Format(Textos.CampoObrigatorio, nameof(Cliente.Nome)));

            RuleFor(p => p.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage(string.Format(Textos.CampoObrigatorio, nameof(Cliente.Email)))
                .Must(email => email is not null && email.EhValido())
                .WithMessage(string.Format(Textos.CampoInvalido, nameof(Cliente.Email)));
        });
    }
}
