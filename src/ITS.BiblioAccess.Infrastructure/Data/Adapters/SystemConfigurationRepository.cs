using FluentResults;
using ITS.BiblioAccess.Domain.Entities;
using ITS.BiblioAccess.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ITS.BiblioAccess.Infrastructure.Data.Adapters;

public class SystemConfigurationRepository : ISystemConfigurationRepository
{
    private readonly AppDBContext _context;

    public SystemConfigurationRepository(AppDBContext context)
    {
        _context = context;
    }

    public async Task<Result<SystemConfiguration>> GetAsync(CancellationToken ct)
    {
        try
        {
            var config = await _context.SystemConfigurations.FirstOrDefaultAsync(ct);
            return config is not null ? Result.Ok(config) : Result.Fail("No system configuration found.");
        }
        catch (Exception ex)
        {
            return Result.Fail($"Error retrieving system configuration: {ex.Message}");
        }
    }

    public async Task<Result> SetAsync(SystemConfiguration configuration, CancellationToken ct)
    {
        try
        {
            var existingConfig = await _context.SystemConfigurations.FirstOrDefaultAsync(ct);

            if (existingConfig is not null)
            {
                // Si ya existe, actualizamos la configuración en lugar de crear una nueva.
                _context.Entry(existingConfig).CurrentValues.SetValues(configuration);
            }
            else
            {
                // Si no existe, creamos una nueva.
                await _context.SystemConfigurations.AddAsync(configuration, ct);
            }

            await _context.SaveChangesAsync(ct);
            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail($"Error saving system configuration: {ex.Message}");
        }
    }
}
