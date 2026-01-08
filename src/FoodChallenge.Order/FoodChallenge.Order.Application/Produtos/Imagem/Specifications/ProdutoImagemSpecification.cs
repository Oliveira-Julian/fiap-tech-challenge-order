using FluentValidation;
using FoodChallenge.Common.Validators;
using FoodChallenge.Order.Domain.Globalization;
using FoodChallenge.Order.Domain.Produtos;

namespace FoodChallenge.Order.Application.Produtos.Imagem.Specifications;

public sealed class ProdutoImagemSpecification : FluentValidatorBase<ProdutoImagem>
{
    private static readonly string[] TiposPermitidos = {
        "image/jpeg",
        "image/jpg",
        "image/png",
        "image/webp"
    };
    private const decimal TamanhoMaximoMB = 1m;
    private static readonly decimal TamanhoMaximoEmBytes = TamanhoMaximoMB * 1024 * 1024;

    public ProdutoImagemSpecification()
    {
        RuleFor(p => p.Tipo)
            .Cascade(CascadeMode.Stop)
            .Must(tipo => TiposPermitidos.Contains(tipo.ToLower()))
            .WithMessage(string.Format(Textos.ImagemTipoInvalido, string.Join(", ", TiposPermitidos)));

        RuleFor(p => p.Conteudo)
            .NotNull()
            .WithMessage(string.Format(Textos.CampoObrigatorio, nameof(ProdutoImagem.Conteudo)))
            .Must(c => c.Length <= TamanhoMaximoEmBytes)
            .WithMessage(string.Format(Textos.ImagemTamanhoExcedido, TamanhoMaximoMB));
    }
}
