using RegistroTecnicos.DAL;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using RegistroTecnicos.Models;
using Microsoft.Identity.Client;


namespace RegistroTecnicos.Services;

public class ClientesService
{
    private readonly IDbContextFactory<Contexto> _DbFactory;
    private readonly ILogger<ClientesService> _Logger;

    public ClientesService(IDbContextFactory<Contexto> DbFactory, ILogger<ClientesService> logger)
    {
        _DbFactory = DbFactory;
        _Logger = logger;
    }

    private async Task<bool> Existe(int ClienteId)
    {
        
        await using var contexto = await _DbFactory.CreateDbContextAsync();
        return await contexto.Clientes
            .AnyAsync(c => c.ClienteId == ClienteId);
    }

    public async Task<bool> Existe(int clienteId, string nombres, string Rnc)
    {

        await using var contexto = await _DbFactory.CreateDbContextAsync();
        return await contexto.Clientes
           .AnyAsync(c => c.ClienteId != clienteId
                && (c.Nombres.ToLower() == nombres.ToLower()
                || c.Rnc == Rnc));
    }

    private async Task<bool> Insertar(Clientes cliente)
    {
        Console.WriteLine("Usando insertar ");
        await using var contexto = await _DbFactory.CreateDbContextAsync();
        contexto.Clientes.Add(cliente);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Clientes cliente)
    {
        Console.WriteLine("Usando Modificar ");
        await using var contexto = await _DbFactory.CreateDbContextAsync();
        contexto.Update(cliente);
        return await contexto
            .SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Clientes cliente)
    {
        Console.WriteLine("Usando GUARDAR ");
        if (!await Existe(cliente.ClienteId))
        {
            return await Insertar(cliente);
        }
        else
        {
            return await Modificar(cliente);
        }
    }

    public async Task<Clientes?> Buscar(int ClienteId)
    {
        Console.WriteLine("Usando BUSCAR ");
        await using var contexto = await _DbFactory.CreateDbContextAsync();
        return await contexto.Clientes
            .FirstOrDefaultAsync(c => c.ClienteId == ClienteId);
    }

    public async Task<bool> Eliminar(int ClienteId)
    {
        Console.WriteLine("Usando ELIMINAR ");
        await using var contexto = await _DbFactory.CreateDbContextAsync();
        return await contexto.Clientes
            .AsNoTracking()
            .Where(c => c.ClienteId == ClienteId)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Clientes>> Listar(Expression<Func<Clientes, bool>> criterio)
    {
        _Logger.LogInformation("Usando Listar");
        await using var contexto = await _DbFactory.CreateDbContextAsync();
        return await contexto.Clientes
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }
}
