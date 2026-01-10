using Microsoft.AspNetCore.Http;

namespace FoodChallenge.Order.Application.Produtos.Imagem.Models.Requests
{
    public sealed class ProdutoImagemRequest
    {
        public IEnumerable<IFormFile> Imagens { get; set; }
    }
}
