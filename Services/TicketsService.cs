using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class TicketsService
{
    private readonly IDbContextFactory<Contexto> _DbFactory;
    private readonly ILogger<TicketsService> _Logger;

    public TicketsService(IDbContextFactory<Contexto> DbFactory, ILogger<TicketsService> logger)
    {
        _DbFactory = DbFactory;
        _Logger = logger;
    }

    private async Task<bool> Existe(int TicketId)
    {
        await using var contexto = await _DbFactory.CreateDbContextAsync();
        return await contexto.Tickets.AnyAsync(t => t.TicketId == TicketId);
    }

    public async Task<bool> Existe(int ticketId, string asunto)
    {
        await using var contexto = await _DbFactory.CreateDbContextAsync();
        return await contexto.Tickets.AnyAsync(t => t.TicketId != ticketId && t.Asunto.ToLower() == asunto.ToLower());
    }

    private async Task<bool> Insertar(Tickets ticket)
    {
        _Logger.LogInformation("Insertando nuevo ticket");
        await using var contexto = await _DbFactory.CreateDbContextAsync();
        contexto.Tickets.Add(ticket);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Tickets ticket)
    {
        _Logger.LogInformation("Modificando ticket existente");
        await using var contexto = await _DbFactory.CreateDbContextAsync();
        contexto.Update(ticket);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Tickets ticket)
    {
        _Logger.LogInformation("Guardando ticket");
        if (!await Existe(ticket.TicketId))
        {
            return await Insertar(ticket);
        }
        else
        {
            return await Modificar(ticket);
        }
    }

    public async Task<Tickets?> Buscar(int TicketId)
    {
        _Logger.LogInformation("Buscando ticket");
        await using var contexto = await _DbFactory.CreateDbContextAsync();
        return await contexto.Tickets.FirstOrDefaultAsync(t => t.TicketId == TicketId);
    }

    public async Task<bool> Eliminar(int TicketId)
    {
        _Logger.LogInformation("Eliminando ticket");
        await using var contexto = await _DbFactory.CreateDbContextAsync();
        return await contexto.Tickets.AsNoTracking().Where(t => t.TicketId == TicketId).ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Tickets>> Listar(Expression<Func<Tickets, bool>> criterio)
    {
        _Logger.LogInformation("Listando tickets");
        await using var contexto = await _DbFactory.CreateDbContextAsync();
        return await contexto.Tickets.Where(criterio).AsNoTracking().ToListAsync();
    }
}
