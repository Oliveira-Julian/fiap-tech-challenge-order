using FoodChallenge.Order.Domain.Clientes;
using FoodChallenge.Order.Domain.Entities;
using FoodChallenge.Order.Domain.Enums;
using FoodChallenge.Order.Domain.Extensions;
using FoodChallenge.Payment.Domain.Pagamentos;

namespace FoodChallenge.Order.Domain.Pedidos;

public class Pedido : DomainBase
{
    public Guid? IdCliente { get; set; }
    public Cliente Cliente { get; set; }
    public Guid? IdPagamento { get; set; }
    public string Codigo { get; set; }
    public Pagamento Pagamento { get; set; }
    public IEnumerable<PedidoItem> Itens { get; set; }
    public decimal ValorTotal { get; set; }
    public PedidoStatus Status { get; set; }
    public bool PodeSerPago() => Status == PedidoStatus.Recebido;

    public void Cadastrar(Guid? idCliente)
    {
        Status = PedidoStatus.Recebido;
        Ativo = true;
        DataCriacao = DateTime.UtcNow;
        IdCliente = idCliente;
        Codigo = StringExtensions.GetRandomCode(6);
    }

    public void AtualizarItens(ICollection<PedidoItem> itens)
    {
        Itens = itens;
    }

    public void AtualizarStatusPedido(PedidoStatus status)
    {
        Status = status;
        Atualizar();
    }

    public void AdicionarPagamento(Pagamento pagamento)
    {
        Pagamento = pagamento;
    }

    public void AtualizarValorTotal() => ValorTotal = Itens?.Sum(item => item.Valor * item.Quantidade) ?? 0;

    public void AtualizarStatusPago()
    {
        Status = PedidoStatus.NaFila;
        Atualizar();
    }

    public bool PermitirAlterarStatus(PedidoStatus status)
    {
        return Status switch
        {
            PedidoStatus.Recebido => status == PedidoStatus.NaFila,
            PedidoStatus.NaFila => status == PedidoStatus.EmPreparacao,
            PedidoStatus.EmPreparacao => status == PedidoStatus.AguardandoRetirada,
            PedidoStatus.AguardandoRetirada => status == PedidoStatus.Finalizado,
            _ => false
        };
    }

    public static IEnumerable<PedidoStatus> ObterTodosStatusNaoFinalizados()
    {
        return Enum.GetValues<PedidoStatus>()
            .Where(s => s != PedidoStatus.Finalizado);
    }
    public bool ContemTodosProdutos(IEnumerable<Guid> idProdutos)
    {
        var idsItens = Itens?.Select(p => p.IdProduto).ToList() ?? new();
        return idProdutos.All(id => idsItens.Contains(id));
    }

    public IEnumerable<Guid> ObterProdutosFaltando(IEnumerable<Guid> idProdutos)
    {
        var idsItens = Itens?.Select(p => p.IdProduto).ToList() ?? new();
        return idProdutos.Where(id => !idsItens.Contains(id));
    }
}
