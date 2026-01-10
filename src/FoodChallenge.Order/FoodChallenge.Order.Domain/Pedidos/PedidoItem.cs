using FoodChallenge.Order.Domain.Entities;
using FoodChallenge.Order.Domain.Extensions;
using FoodChallenge.Order.Domain.Produtos;

namespace FoodChallenge.Order.Domain.Pedidos;

public class PedidoItem : DomainBase
{
    public Guid? IdProduto { get; set; }
    public Guid? IdPedido { get; set; }
    public string Codigo { get; set; }
    public decimal Valor { get; set; }
    public int Quantidade { get; set; }
    public virtual Produto Produto { get; set; }

    public PedidoItem()
    {
        Id = Guid.NewGuid();
        Ativo = true;
        DataCriacao = DateTime.UtcNow;
        Codigo = StringExtensions.GetRandomCode(6);
    }

    public PedidoItem AtualizarValor(decimal valor)
    {
        Valor = valor;
        return this;
    }
}
