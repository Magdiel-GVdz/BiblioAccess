using ITS.BiblioAccess.Application;
using ITS.BiblioAccess.Infrastructure;
using ITS.BiblioAccess.Presentation.Forms;
using ITS.BiblioAccess.Presentation.Forms.Careers;
using ITS.BiblioAccess.Presentation.Forms.EntryRecors;
using ITS.BiblioAccess.Presentation.Forms.Reports;
using ITS.BiblioAccess.Presentation.Forms.SystemConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


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
        services.AddTransient<AddCareerForm>();
        services.AddTransient<CareersForm>();
        services.AddTransient<SystemConfigurationForm>();
        services.AddTransient<EditCareerForm>();
        services.AddTransient<EntryRecordForm>();
        services.AddTransient<ExcelExporter>();
        services.AddTransient<ExcelReportsForm>();

    }
}
