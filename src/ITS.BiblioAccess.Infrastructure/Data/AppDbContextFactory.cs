using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace ITS.BiblioAccess.Infrastructure.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDBContext>
{
    public AppDBContext CreateDbContext(string[] args)
    {
        // Buscar la ruta del proyecto de presentación (donde está appsettings.json)
        string basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "ITS.BiblioAccess.Presentation");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath) // Apuntar a la capa de Presentation
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        // Obtener la cadena de conexión
        string dbPath = configuration["DatabaseSettings:DatabasePath"]!;

        if (string.IsNullOrEmpty(dbPath))
        {
            dbPath = "C:/BiblioAccess/biblioaccess.db"; // Ruta por defecto si no está configurada
        }

        // Configurar SQLite
        var optionsBuilder = new DbContextOptionsBuilder<AppDBContext>();
        optionsBuilder.UseSqlite($"Data Source={dbPath}");

        return new AppDBContext(optionsBuilder.Options);
    }
}
