using ITS.BiblioAccess.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ITS.BiblioAccess.Application;
using ITS.BiblioAccess.Infrastructure;


namespace ITS.BiblioAccess.Presentation;

public static class Startup
{
    public static void ConfigureServices(IServiceCollection services, string dbPath)
    {
        // Configurar DbContext con SQLite
        services.AddDbContext<AppDBContext>(options =>
            options.UseSqlite($"Data Source={dbPath}"));

        // Registrar dependencias de Infrastructure y Application
        services.AddInfrastructure();
        services.AddApplication();

        // Registrar MainForm y otros formularios
        services.AddTransient<MainForm>();
    }
}
