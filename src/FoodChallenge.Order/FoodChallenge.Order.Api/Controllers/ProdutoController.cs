using FoodChallenge.Common.Entities;
using FoodChallenge.CrossCutting.Paging;
using FoodChallenge.Order.Adapter.Controllers;
using FoodChallenge.Order.Application.Produtos.Models.Reponses;
using FoodChallenge.Order.Application.Produtos.Models.Requests;
using FoodChallenge.Order.Domain.Constants;
using Microsoft.AspNetCore.Mvc;

namespace FoodChallenge.Order.Api.Controllers;

[ApiController]
[Route("[Controller]")]
public class ProdutoController(
    ProdutoAppController produtoAppController,
    ILogger<ProdutoController> logger) : ControllerBase
{
    /// <summary>
    /// Cadastrar Produtos.
    /// </summary>
    /// <param name="request">Informações da requição.</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    [HttpPost]
    [ProducesResponseType(typeof(Resposta<ProdutoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Resposta), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Resposta), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(Resposta), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Resposta>> CadastrarAsync([FromBody] IEnumerable<ProdutoRequest> request, CancellationToken cancellationToken)
    {
        logger.LogInformation(Logs.InicioExecucaoServico, nameof(ProdutoController), nameof(CadastrarAsync));

        var resposta = await produtoAppController.CadastrarProdutoAsync(request, cancellationToken);

        logger.LogDebug(Logs.FimExecucaoServico, nameof(ProdutoController), nameof(CadastrarAsync), resposta);

        return resposta is null ? NoContent() : Ok(resposta);
    }

    /// <summary>
    /// Editar Produto.
    /// </summary>
    /// <param name="id">Identificador do produto.</param>
    /// <param name="request">Informações da requição.</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Resposta<ProdutoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Resposta), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Resposta), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(Resposta), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Resposta>> EditarAsync(Guid id, [FromBody] ProdutoRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation(Logs.InicioExecucaoServico, nameof(ProdutoController), nameof(EditarAsync));

        var resposta = await produtoAppController.AtualizarProdutoAsync(id, request, cancellationToken);

        logger.LogDebug(Logs.FimExecucaoServico, nameof(ProdutoController), nameof(EditarAsync), resposta);

        return resposta is null ? NoContent() : Ok(resposta);
    }

    /// <summary>
    /// Remover Produto.
    /// </summary>
    /// <param name="id">Identificador do produto.</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(Resposta), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Resposta), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Resposta), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(Resposta), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Resposta>> RemoverAsync(Guid id, CancellationToken cancellationToken)
    {
        logger.LogInformation(Logs.InicioExecucaoServico, nameof(ProdutoController), nameof(RemoverAsync));

        var resposta = await produtoAppController.RemoverProdutoAsync(id, cancellationToken);

        logger.LogDebug(Logs.FimExecucaoServico, nameof(ProdutoController), nameof(RemoverAsync), resposta);

        return resposta is null ? NoContent() : Ok(resposta);
    }

    /// <summary>
    /// Consultar Produto por Id.
    /// </summary>
    /// <param name="id">Identificador do produto.</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Resposta<ProdutoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Resposta), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Resposta), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(Resposta), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Resposta>> ConsultarPorIdAsync(Guid id, CancellationToken cancellationToken)
    {
        logger.LogInformation(Logs.InicioExecucaoServico, nameof(ProdutoController), nameof(ConsultarPorIdAsync));

        var resposta = await produtoAppController.ObterProdutoAsync(id, cancellationToken);

        logger.LogDebug(Logs.FimExecucaoServico, nameof(ProdutoController), nameof(ConsultarPorIdAsync), resposta);

        return resposta is null ? NoContent() : Ok(resposta);
    }

    /// <summary>
    /// Buscar Produtos.
    /// </summary>
    /// <param name="request">Informações de filtro da requição.</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    [HttpGet]
    [ProducesResponseType(typeof(Resposta<Pagination<ProdutoResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Resposta), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Resposta), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(Resposta), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Resposta>> BuscarAsync([FromQuery] FiltrarProdutoRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation(Logs.InicioExecucaoServico, nameof(ProdutoController), nameof(BuscarAsync));

        var resposta = await produtoAppController.ObterPorFiltroAsync(request, cancellationToken);

        logger.LogDebug(Logs.FimExecucaoServico, nameof(ProdutoController), nameof(BuscarAsync), resposta);

        return resposta is null ? NoContent() : Ok(resposta);
    }
}
