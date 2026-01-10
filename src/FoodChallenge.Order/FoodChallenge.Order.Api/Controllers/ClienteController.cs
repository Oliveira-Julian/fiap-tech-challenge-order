using FoodChallenge.Common.Entities;
using FoodChallenge.CrossCutting.Paging;
using FoodChallenge.Order.Adapter.Controllers;
using FoodChallenge.Order.Application.Clientes.Models.Requests;
using FoodChallenge.Order.Application.Clientes.Models.Responses;
using FoodChallenge.Order.Domain.Constants;
using Microsoft.AspNetCore.Mvc;

namespace FoodChallenge.Order.Api.Controllers;

[ApiController]
[Route("[Controller]")]
public class ClienteController(
    ILogger<ClienteController> logger,
    ClienteAppController clienteAppController) : ControllerBase
{
    /// <summary>
    /// Buscar Clientes.
    /// </summary>
    /// <param name="request">Informações de filtro da requição.</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    [HttpGet]
    [ProducesResponseType(typeof(Resposta<Pagination<ClienteResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Resposta), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Resposta), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(Resposta), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Resposta>> BuscarAsync([FromQuery] FiltrarClienteRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation(Logs.InicioExecucaoServico, nameof(ClienteController), nameof(BuscarAsync));

        var resposta = await clienteAppController.PesquisarClienteAsync(request, cancellationToken);

        logger.LogDebug(Logs.FimExecucaoServico, nameof(ClienteController), nameof(BuscarAsync), resposta);

        return resposta is null ? NoContent() : Ok(resposta);
    }
}
