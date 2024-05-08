﻿using Microsoft.EntityFrameworkCore;
using ProyectoTecnicos.Models;

namespace ProyectoTecnicos.DAL;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options)
        : base(options) { }

    public DbSet<Tecnicos> Tecnicos { get; set;}
}