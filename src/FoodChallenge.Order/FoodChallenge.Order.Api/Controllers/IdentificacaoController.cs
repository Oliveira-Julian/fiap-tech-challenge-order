using FoodChallenge.Common.Entities;
using FoodChallenge.Order.Adapter.Controllers;
using FoodChallenge.Order.Application.Clientes.Models.Requests;
using FoodChallenge.Order.Application.Clientes.Models.Responses;
using FoodChallenge.Order.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodChallenge.Order.Api.Controllers;

[ApiController]
[Route("[Controller]")]
[Authorize]
public class IdentificacaoController(
    ILogger<IdentificacaoController> logger,
    ClienteAppController clienteAppController) : ControllerBase
{
    /// <summary>
    /// Registrar Cliente.
    /// </summary>
    /// <param name="request">Informações da requição.</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    [HttpPost("registrar")]
    [ProducesResponseType(typeof(Resposta<ClienteResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Resposta), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Resposta), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(Resposta), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegistrarAsync(RegistrarClienteRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation(Logs.InicioExecucaoServico, nameof(IdentificacaoController), nameof(RegistrarAsync));

        var resposta = await clienteAppController.RegistrarAsync(request, cancellationToken);

        logger.LogDebug(Logs.FimExecucaoServico, nameof(IdentificacaoController), nameof(RegistrarAsync), resposta);

        return resposta is null ? NoContent() : Ok(resposta);
    }
}
