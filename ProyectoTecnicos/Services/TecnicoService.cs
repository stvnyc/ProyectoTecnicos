using Microsoft.EntityFrameworkCore;
using ProyectoTecnicos.DAL;
using ProyectoTecnicos.Models;
using System.Linq.Expressions;


namespace ProyectoTecnicos.Services;

public class TecnicoService
{
    private readonly Contexto _context;
    public TecnicoService (Contexto contexto)
    {
        _context = contexto; 
    }

    public async Task<bool> Existe(int tecnicoId)
    {
        return await _context.Tecnicos
            .AnyAsync(t => t.TecnicoId == tecnicoId);
    }

    private async Task<bool> Insertar(Tecnicos tecnico)
    {
        _context.Tecnicos.Add(tecnico);
        return await _context.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Tecnicos tecnico)
    {
        _context.Tecnicos.Update(tecnico);
        var modificado = await _context.SaveChangesAsync() > 0;
        _context.Entry(tecnico).State = EntityState.Detached;
        return modificado;
    }

    public async Task<bool> Guardar(Tecnicos tecnico)
    {
        if (_context.Tecnicos!.Any(t => t.Nombres!.ToLower().Replace(" ", "") == tecnico.Nombres!.ToLower().Replace(" ", "")
        && t.TecnicoId != tecnico.TecnicoId))
        {
            return false;
        }
        if (!await Existe(tecnico.TecnicoId))
            return await Insertar(tecnico);
        else
            return await Modificar(tecnico);
    }

    public async Task<bool> Eliminar(int id)
    {
        var tecnicos = await _context.Tecnicos
            .Where(t => t.TecnicoId == id)
            .ExecuteDeleteAsync();
        return tecnicos > 0;
    }

    public async Task<Tecnicos?> Buscar(int id)
    {
        return await _context.Tecnicos
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TecnicoId == id);
    }

    public async Task<List<Tecnicos>> Listar(Expression<Func<Tecnicos, bool>> criterio)
    {
        return await _context.Tecnicos
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}
