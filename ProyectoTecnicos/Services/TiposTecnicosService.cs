﻿using Microsoft.EntityFrameworkCore;
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

    public async Task<bool> Existe(int tipoId, string descripcion)
    {
        return await _context.TiposTecnicos
            .AnyAsync(t => t.TipoId != tipoId && t.Descripcion.Equals(descripcion));
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
            .Include(t => t.Tecnicos)
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }

    public async Task MontoIncentivos()
    {
        var tiposTecnicos = await _context.TiposTecnicos.ToListAsync();

        foreach (var tipo in tiposTecnicos)
        {
            tipo.Incentivo = await CalcularMontoTIncentivos(tipo.TipoId);
        }
        await _context.SaveChangesAsync();
    }

    public async Task<decimal> CalcularMontoTIncentivos(int tipoId)
    {
        var montoTotal = await _context.Incentivos
            .Where(i => i.Tecnicos.idTipo == tipoId)
            .SumAsync(i => (double)i.Monto);

        return (decimal)montoTotal;
    }

    public async Task<List<TiposTecnicos>> ListaTotal()
    {
        var tiposTecnicos = await _context.TiposTecnicos
            .Include(t => t.Tecnicos)
            .ToListAsync();

        foreach (var tipo in tiposTecnicos)
        {
            tipo.Incentivo = await CalcularMontoTIncentivos(tipo.TipoId);
        }
        return tiposTecnicos;
    }
}