using ITS.BiblioAccess.Infrastructure.Data;
using ITS.BiblioAccess.Presentation.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ITS.BiblioAccess.Presentation
{
    static class Program
    {
        private static IConfiguration _configuration;

        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            // Cargar configuraci�n desde appsettings.json
            _configuration = LoadConfiguration();

            // Obtener o crear la base de datos en la ruta especificada
            string dbPath = GetOrCreateDatabasePath();

            var host = CreateHostBuilder(dbPath).Build();

            // Ejecutar migraciones para asegurarse de que la base de datos est� creada
            ApplyMigrations(host);

            var services = host.Services;
            var mainForm = services.GetRequiredService<MainForm>();
            System.Windows.Forms.Application.Run(mainForm);
        }

        static IHostBuilder CreateHostBuilder(string dbPath) =>
            Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    Startup.ConfigureServices(services, dbPath);
                });

        static IConfiguration LoadConfiguration()
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return configBuilder.Build();
        }

        static string GetOrCreateDatabasePath()
        {
            string dbPath = _configuration["DatabaseSettings:DatabasePath"];

            // Si la configuraci�n no tiene una ruta, usa la predeterminada
            if (string.IsNullOrWhiteSpace(dbPath))
            {
                string defaultDirectory = "C:/BiblioAccess";

                // Asegurar que la carpeta existe antes de usarla
                if (!Directory.Exists(defaultDirectory))
                {
                    Directory.CreateDirectory(defaultDirectory);
                }

                dbPath = Path.Combine(defaultDirectory, "biblioaccess.db");

                // Guardar la nueva configuraci�n
                SaveDatabasePath(dbPath);
            }
            else
            {
                // Obtener la carpeta de la ruta configurada
                string? directoryPath = Path.GetDirectoryName(dbPath);

                // Asegurar que la carpeta existe antes de intentar usar el archivo
                if (!string.IsNullOrWhiteSpace(directoryPath) && !Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
            }

            // Verificar si el archivo de la base de datos existe, si no, SQLite lo crear� con migraciones
            if (!File.Exists(dbPath))
            {
                Console.WriteLine($"El archivo de la base de datos no existe, SQLite lo crear� en: {dbPath}");
            }

            return dbPath;
        }



        static void SaveDatabasePath(string dbPath)
        {
            string configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");

            var newConfig = new
            {
                DatabaseSettings = new
                {
                    DatabasePath = dbPath
                }
            };

            File.WriteAllText(configFile, System.Text.Json.JsonSerializer.Serialize(newConfig, new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));
        }

        static void ApplyMigrations(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<AppDBContext>();

                // Aplicar migraciones para asegurarse de que la base de datos se crea
                dbContext.Database.Migrate();
            }
        }
    }
}
