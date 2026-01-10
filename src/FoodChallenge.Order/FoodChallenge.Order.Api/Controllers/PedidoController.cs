using FoodChallenge.Common.Entities;
using FoodChallenge.Order.Adapter.Controllers;
using FoodChallenge.Order.Application.Pedidos.Models.Requests;
using FoodChallenge.Order.Application.Pedidos.Models.Responses;
using FoodChallenge.Order.Domain.Constants;
using Microsoft.AspNetCore.Mvc;

namespace FoodChallenge.Order.Api.Controllers;

[ApiController]
[Route("[Controller]")]
public class PedidoController(
    PedidoAppController pedidoAppController,
    ILogger<PedidoController> logger) : ControllerBase
{
    /// <summary>
    /// Cadastrar Pedido.
    /// </summary>
    /// <param name="request">Informações da requição.</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    [HttpPost]
    [ProducesResponseType(typeof(Resposta<PedidoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Resposta), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Resposta), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(Resposta), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Resposta>> CadastrarAsync([FromBody] CadastrarPedidoRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation(Logs.InicioExecucaoServico, nameof(PedidoController), nameof(CadastrarAsync));

        var resposta = await pedidoAppController.CadastrarAsync(request, cancellationToken);

        logger.LogDebug(Logs.FimExecucaoServico, nameof(PedidoController), nameof(CadastrarAsync), resposta);

        return resposta is null ? NoContent() : Ok(resposta);
    }

    /// <summary>
    /// Consultar Pedido por Id.
    /// </summary>
    /// <param name="id">Identificador do pedido.</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Resposta<PedidoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Resposta), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Resposta), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(Resposta), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Resposta>> ConsultarPedidoPorIdAsync(Guid id, CancellationToken cancellationToken)
    {
        logger.LogInformation(Logs.InicioExecucaoServico, nameof(PedidoController), nameof(ConsultarPedidoPorIdAsync));

        var resposta = await pedidoAppController.ObterPedidoAsync(id, cancellationToken);

        logger.LogDebug(Logs.FimExecucaoServico, nameof(PedidoController), nameof(ConsultarPedidoPorIdAsync), resposta);

        return resposta is null ? NoContent() : Ok(resposta);
    }

    /// <summary>
    /// Atualizar Status do Pedido por Id.
    /// </summary>
    /// <param name="id">Identificador do pedido.</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    [HttpPut("{id}/finalizar")]
    [ProducesResponseType(typeof(Resposta<PedidoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Resposta), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Resposta), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(Resposta), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Resposta>> AtualizarStatusPorIdAsync(Guid id, CancellationToken cancellationToken)
    {
        logger.LogInformation(Logs.InicioExecucaoServico, nameof(PedidoController), nameof(AtualizarStatusPorIdAsync));

        var resposta = await pedidoAppController.FinalizarPedidoAsync(id, cancellationToken);

        logger.LogDebug(Logs.FimExecucaoServico, nameof(PedidoController), nameof(AtualizarStatusPorIdAsync), resposta);

        return resposta is null ? NoContent() : Ok(resposta);
    }
}
