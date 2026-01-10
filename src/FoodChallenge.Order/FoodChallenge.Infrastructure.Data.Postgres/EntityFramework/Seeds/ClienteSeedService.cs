using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Context;
using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Clientes;
using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Clientes.Interfaces;
using Microsoft.Extensions.Logging;

namespace FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Seeds;

public class ClienteSeedService(
    ILogger<ClienteSeedService> logger,
    EntityFrameworkContext context) : IClienteSeedService
{
    public async Task SeedAsync()
    {
        await SeedClienteNaoIdentificadoAsync();
    }

    private async Task SeedClienteNaoIdentificadoAsync()
    {
        try
        {
            const string clienteNaoIdentificado = "NÃO IDENTIFICADO";

            var clienteExistente = context.Cliente
                .FirstOrDefault(c => c.Nome != null && c.Nome.ToLower() == clienteNaoIdentificado.ToLower());

            if (clienteExistente is not null)
                return;

            var cliente = new ClienteEntity()
            {
                Id = Guid.Parse("a0c7ec9b-33ac-43df-85dd-4c38cadb4cda"),
                Nome = clienteNaoIdentificado
            };

            await context.Cliente.AddAsync(cliente);
            await context.SaveChangesAsync();

            logger.LogInformation("Cliente padrão 'NÃO IDENTIFICADO' criado com sucesso.");
        }
        catch { /* ignora caso ocorra alguma exceção */ }
    }
}
