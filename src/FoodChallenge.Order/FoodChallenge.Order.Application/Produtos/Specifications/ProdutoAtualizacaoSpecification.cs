using FluentValidation;
using FoodChallenge.Common.Validators;
using FoodChallenge.Order.Domain.Globalization;
using FoodChallenge.Order.Domain.Produtos;

namespace FoodChallenge.Order.Application.Produtos.Specifications;

public sealed class ProdutoAtualizacaoSpecification : FluentValidatorBase<Produto>
{
    public ProdutoAtualizacaoSpecification()
    {
        RuleFor(p => p.Id)
            .NotEmpty()
            .WithMessage(string.Format(Textos.CampoObrigatorio, nameof(Produto.Id)));

        RuleFor(p => p.Categoria)
            .NotEmpty()
            .WithMessage(string.Format(Textos.CampoObrigatorio, nameof(Produto.Categoria)))
            .IsInEnum()
            .WithMessage(string.Format(Textos.CampoInvalido, nameof(Produto.Categoria)));

        RuleFor(p => p.Nome)
            .NotEmpty()
            .WithMessage(string.Format(Textos.CampoObrigatorio, nameof(Produto.Nome)));

        RuleFor(p => p.Descricao)
            .NotEmpty()
            .WithMessage(string.Format(Textos.CampoObrigatorio, nameof(Produto.Descricao)));

        RuleFor(p => p.Preco)
            .NotEmpty()
            .WithMessage(string.Format(Textos.CampoObrigatorio, nameof(Produto.Preco)))
            .GreaterThan(0)
            .WithMessage(string.Format(Textos.CampoObrigatorio, nameof(Produto.Preco)));
    }
}
