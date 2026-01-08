using FoodChallenge.Common.Interfaces;
using FoodChallenge.Common.Validators;
using FoodChallenge.Order.Application.Clientes.Interfaces;
using FoodChallenge.Order.Application.Clientes.Specifications;
using FoodChallenge.Order.Domain.Clientes;
using FoodChallenge.Order.Domain.Constants;
using Serilog;

namespace FoodChallenge.Order.Application.Clientes.UseCases;

public sealed class RegistraClienteUseCase(
    ValidationContext validationContext,
    IUnitOfWork unitOfWork,
    IClienteGateway clienteGateway) : IRegistraClienteUseCase
{
    private readonly ILogger logger = Log.ForContext<RegistraClienteUseCase>();

    public async Task<Cliente> ExecutarAsync(Cliente cliente, CancellationToken cancellationToken)
    {
        logger.Information(Logs.InicioExecucaoServico, nameof(RegistraClienteUseCase), nameof(ExecutarAsync));

        try
        {
            await validationContext.AddValidationsAsync(cliente, cancellationToken, new ClienteRegistrarSpecification(clienteGateway));

            if (validationContext.HasValidations)
                return default;

            cliente.Cadastrar();

            unitOfWork.BeginTransaction();
            var clienteCadastrado = await clienteGateway.CadastrarClienteAsync(cliente, cancellationToken);
            await unitOfWork.CommitAsync();

            logger.Information(Logs.FimExecucaoServico, nameof(RegistraClienteUseCase), nameof(ExecutarAsync), clienteCadastrado);

            return clienteCadastrado;
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackAsync();
            logger.Error(ex, Logs.ErroGenerico, nameof(RegistraClienteUseCase), nameof(ExecutarAsync));
            throw;
        }
    }
}
