using RegistroTecnicos.DAL;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using RegistroTecnicos.Components.Models;
using Microsoft.Extensions.Logging;

namespace RegistroTecnicos.Services;

public class SistemasService
{
    private readonly IDbContextFactory<Contexto> _DbFactory;
    private readonly ILogger<SistemasService> _Logger;

    public SistemasService(IDbContextFactory<Contexto> DbFactory, ILogger<SistemasService> logger)
    {
        _DbFactory = DbFactory;
        _Logger = logger;
    }

    private async Task<bool> Existe(int sistemaId)
    {
        await using var contexto = await _DbFactory.CreateDbContextAsync();
        return await contexto.Sistemas.AnyAsync(s => s.SistemaId == sistemaId);
    }

    private async Task<bool> Insertar(Sistemas sistema)
    {
        _Logger.LogInformation("Insertando nuevo sistema");
        await using var contexto = await _DbFactory.CreateDbContextAsync();
        contexto.Sistemas.Add(sistema);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Sistemas sistema)
    {
        _Logger.LogInformation("Modificando sistema existente");
        await using var contexto = await _DbFactory.CreateDbContextAsync();
        contexto.Update(sistema);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Sistemas sistema)
    {
        _Logger.LogInformation("Guardando sistema");
        if (!await Existe(sistema.SistemaId))
        {
            return await Insertar(sistema);
        }
        else
        {
            return await Modificar(sistema);
        }
    }

    public async Task<Sistemas?> Buscar(int sistemaId)
    {
        _Logger.LogInformation("Buscando sistema");
        await using var contexto = await _DbFactory.CreateDbContextAsync();
        return await contexto.Sistemas.FirstOrDefaultAsync(s => s.SistemaId == sistemaId);
    }

    public async Task<bool> Eliminar(int sistemaId)
    {
        _Logger.LogInformation("Eliminando sistema");
        await using var contexto = await _DbFactory.CreateDbContextAsync();
        return await contexto.Sistemas
            .AsNoTracking()
            .Where(s => s.SistemaId == sistemaId)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Sistemas>> Listar(Expression<Func<Sistemas, bool>> criterio)
    {
        _Logger.LogInformation("Listando sistemas");
        await using var contexto = await _DbFactory.CreateDbContextAsync();
        return await contexto.Sistemas
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }
}
