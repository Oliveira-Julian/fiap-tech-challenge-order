namespace FoodChallenge.Order.Application.Produtos.Imagem.Models.Responses
{
    public sealed class ProdutoImagemResponse
    {
        public Guid? Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public decimal Tamanho { get; set; }
        public byte[] Conteudo { get; set; }
    }
}
