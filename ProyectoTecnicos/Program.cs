using Microsoft.EntityFrameworkCore;
using ProyectoTecnicos.Components;
using ProyectoTecnicos.DAL;
using ProyectoTecnicos.Services;

namespace ProyectoTecnicos;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        //Obtenemos el ConStr
        var ConStr = builder.Configuration.GetConnectionString("ConStr");

        //Agregamos el contexto al builder
        builder.Services.AddDbContext<Contexto>(Options => Options.UseSqlite(ConStr));

        builder.Services.AddScoped<TecnicoService>();

        builder.Services.AddBlazorBootstrap();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}
