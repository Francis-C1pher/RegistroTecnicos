using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class TecnicosService
{
    private readonly IDbContextFactory<Contexto> _DbFactory;
    private readonly ILogger<TecnicosService> _Logger;

    public TecnicosService(IDbContextFactory<Contexto> DbFactory, ILogger<TecnicosService> logger)
    {
        _DbFactory = DbFactory;
        _Logger = logger;
    }

    private async Task<bool> Existe(int TecnicoId)
    {
        await using var contexto = await _DbFactory.CreateDbContextAsync();
        return await contexto.Tecnicos
            .AnyAsync(p => p.TecnicoId == TecnicoId);
    }
    public async Task<bool> Existe(int tecnicoId, string nombres)
    {
        await using var contexto = await _DbFactory.CreateDbContextAsync();
        return await contexto.Tecnicos
            .AnyAsync(t => t.TecnicoId != tecnicoId && (t.Nombres.ToLower() == nombres.ToLower()));
    }

    private async Task<bool> Insertar(Tecnicos tecnico)
    {
        Console.WriteLine("Usando insertar ");
        await using var contexto = await _DbFactory.CreateDbContextAsync();
        contexto.Tecnicos.Add(tecnico);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Tecnicos tecnico)
    {
        Console.WriteLine("Usando Modificar ");
        await using var contexto = await _DbFactory.CreateDbContextAsync();
        contexto.Update(tecnico);
        return await contexto
            .SaveChangesAsync() > 0;
    }


    public async Task<bool> Guardar(Tecnicos tecnico)
    {
        Console.WriteLine("Usando GUARDAR ");
        if (!await Existe(tecnico.TecnicoId))
        {
            return await Insertar(tecnico);
        }
        else
        {
            return await Modificar(tecnico);
        }

    }
    public async Task<Tecnicos?> Buscar(int TecnicoId)
    {
        Console.WriteLine("Usando BUSCAR ");
        await using var contexto = await _DbFactory.CreateDbContextAsync();
        return await contexto.Tecnicos.FirstOrDefaultAsync(t => t.TecnicoId == TecnicoId);
    }

    public async Task<bool> Eliminar(int TecnicoId)
    {
        Console.WriteLine("Usando ELIMINAR ");
        await using var contexto = await _DbFactory.CreateDbContextAsync();
        return await contexto.Tecnicos
            .AsNoTracking()
            .Where(t => t.TecnicoId == TecnicoId)
            .ExecuteDeleteAsync() > 0;


    }
    public async Task<List<Tecnicos>> Listar(Expression<Func<Tecnicos, bool>> criterio)
    {
        _Logger.LogInformation("Usando Listar"); //Asi se da un mensaje de verdad en la consola (log)
        await using var contexto = await _DbFactory.CreateDbContextAsync();
        return await contexto.Tecnicos
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }
}
