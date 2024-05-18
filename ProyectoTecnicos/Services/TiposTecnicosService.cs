using Microsoft.EntityFrameworkCore;
using ProyectoTecnicos.DAL;
using ProyectoTecnicos.Models;
using System.Linq.Expressions;

namespace ProyectoTecnicos.Services;

public class TiposTecnicosService
{
    private readonly Contexto _context;
    public TiposTecnicosService(Contexto contexto)
    {
        _context = contexto;
    }

    public async Task<bool> Existe(int TipoId)
    {
        return await _context.TiposTecnicos
            .AnyAsync(t => t.TipoId == TipoId);
    }

    public async Task<bool> Existe(string? descripcion, int? tipoId = null)
    {
        return await _context.TiposTecnicos
            .AnyAsync(t => t.Descripcion.Equals(descripcion));
    }

    private async Task<bool> Insertar(TiposTecnicos tiposTecnicos)
    {
        _context.TiposTecnicos.Add(tiposTecnicos);
        return await _context.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(TiposTecnicos tiposTecnicos)
    {
        _context.TiposTecnicos.Update(tiposTecnicos);
        var modificado = await _context.SaveChangesAsync() > 0;
        _context.Entry(tiposTecnicos).State = EntityState.Detached;
        return modificado;
    }

    public async Task<bool> Guardar(TiposTecnicos tiposTecnicos)
    {
        if (!await Existe(tiposTecnicos.TipoId))
            return await Insertar(tiposTecnicos);
        else
            return await Modificar(tiposTecnicos);
    }

    public async Task<bool> Eliminar(int id)
    {
        var tiposTecnicos = await _context.TiposTecnicos
            .Where(t => t.TipoId == id)
            .ExecuteDeleteAsync();
        return tiposTecnicos > 0;
    }

    public async Task<TiposTecnicos?> Buscar(int id)
    {
        return await _context.TiposTecnicos
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TipoId == id);
    }

    public async Task<List<TiposTecnicos>> Listar(Expression<Func<TiposTecnicos, bool>> criterio)
    {
        return await _context.TiposTecnicos
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}
