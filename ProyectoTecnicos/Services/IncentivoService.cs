using Microsoft.EntityFrameworkCore;
using ProyectoTecnicos.DAL;
using ProyectoTecnicos.Models;
using System.Linq.Expressions;

namespace ProyectoTecnicos.Services;

public class IncentivoService
{
    private readonly Contexto _context;
    public IncentivoService(Contexto contexto)
    {
        _context = contexto;
    }

    public async Task<bool> Existe(int IncentivoId)
    {
        return await _context.Incentivos
            .AnyAsync(i => i.IncentivoId == IncentivoId);
    }

    public async Task<bool> Existe(int incentivoId, string descripcion)
    {
        return await _context.Incentivos
            .AnyAsync(i => i.IncentivoId != incentivoId && i.Descripcion!.Equals(descripcion.ToLower()));
    }

    private async Task<bool> Insertar(Incentivos incentivo)
    {
        _context.Incentivos.Add(incentivo);
        return await _context.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Incentivos incentivo)
    {
        _context.Incentivos.Update(incentivo);
        var modificado = await _context.SaveChangesAsync() > 0;
        _context.Entry(incentivo).State = EntityState.Detached;
        return modificado;
    }

    public async Task<bool> Guardar(Incentivos incentivo)
    {
        if (!await Existe(incentivo.IncentivoId))
            return await Insertar(incentivo);
        else
            return await Modificar(incentivo);
    }

    public async Task<bool> Eliminar(int id)
    {
        var incentivos = await _context.Incentivos
            .Where(i => i.IncentivoId == id)
            .ExecuteDeleteAsync();
        return incentivos > 0;
    }

    public async Task<Incentivos?> Buscar(int id)
    {
        return await _context.Incentivos
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.IncentivoId == id);
    }

    public async Task<List<Incentivos>> Listar(Expression<Func<Incentivos, bool>> criterio)
    {
        return await _context.Incentivos
            .Include(t => t.Tecnicos)
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}