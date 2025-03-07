using ITS.BiblioAccess.Domain.Repositories;
using ITS.BiblioAccess.Infrastructure.Data.Adapters;
using Microsoft.Extensions.DependencyInjection;

namespace ITS.BiblioAccess.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Registrar Repositorios o Unit of Work
        services.AddScoped<ICareerRepository, CareerRepository>();
        services.AddScoped<IEntryRecordRepository, EntryRecordRepository>();
        services.AddScoped<ISystemConfigurationRepository, SystemConfigurationRepository>();

        return services;
    }
}
