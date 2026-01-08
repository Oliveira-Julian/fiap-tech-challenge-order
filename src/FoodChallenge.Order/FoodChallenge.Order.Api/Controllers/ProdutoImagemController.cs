using FoodChallenge.Order.Adapter.Controllers;
using FoodChallenge.Order.Application.Produtos.Imagem.Models.Requests;
using FoodChallenge.Order.Application.Produtos.Models.Reponses;
using FoodChallenge.Common.Entities;
using FoodChallenge.Order.Domain.Constants;
using Microsoft.AspNetCore.Mvc;

namespace FoodChallenge.Order.Api.Controllers;

[ApiController]
[Route("produto/{id}/imagem")]
public class ProdutoImagemController(
    ProdutoAppController produtoAppController,
    ILogger<ProdutoImagemController> logger) : ControllerBase
{
    /// <summary>
    /// Cadastrar Produto.
    /// </summary>
    /// <param name="id">Identificador do produto.</param>
    /// <param name="request">Informações da requição.</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    [HttpPost]
    [ProducesResponseType(typeof(Resposta<ProdutoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Resposta), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Resposta), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(Resposta), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Resposta>> CadastrarAsync(Guid id, [FromForm] ProdutoImagemRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation(Logs.InicioExecucaoServico, nameof(ProdutoImagemController), nameof(CadastrarAsync));

        var resposta = await produtoAppController.CadastrarProdutoImagensAsync(id, request, cancellationToken);

        logger.LogDebug(Logs.FimExecucaoServico, nameof(ProdutoImagemController), nameof(CadastrarAsync), resposta);

        return resposta is null ? NoContent() : Ok(resposta);
    }

    /// <summary>
    /// Remover Produto.
    /// </summary>
    /// <param name="id">Identificador do produto.</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    [HttpDelete("{idImagem}")]
    [ProducesResponseType(typeof(Resposta), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Resposta), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Resposta), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(Resposta), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Resposta>> RemoverAsync(Guid id, Guid idImagem, CancellationToken cancellationToken)
    {
        logger.LogInformation(Logs.InicioExecucaoServico, nameof(ProdutoImagemController), nameof(RemoverAsync));

        var resposta = await produtoAppController.RemoverProdutoImagemAsync(id, idImagem, cancellationToken);

        logger.LogDebug(Logs.FimExecucaoServico, nameof(ProdutoImagemController), nameof(RemoverAsync), resposta);

        return resposta is null ? NoContent() : Ok(resposta);
    }
}
