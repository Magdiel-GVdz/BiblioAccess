using FluentResults;
using ITS.BiblioAccess.Domain.ValueObjects;

namespace ITS.BiblioAccess.Domain.Entities;

public class SystemConfiguration
{
    public Guid SystemConfigurationId { get; private init; }
    public ExportHour ExportHour { get; private set; }

    private SystemConfiguration() { }

    private SystemConfiguration(Guid id, ExportHour exportHour)
    {
        SystemConfigurationId = id;
        ExportHour = exportHour;
    }

    public static Result<SystemConfiguration> Create(TimeOnly exportHour)
    {
        return Create(Guid.NewGuid(), exportHour);
    }

    public static Result<SystemConfiguration> Create(Guid id, TimeOnly exportHour)
    {
        if (id == Guid.Empty)
            return Result.Fail("Configuration ID cannot be empty.");

        return Result.Ok(new SystemConfiguration(id, ExportHour.Create(exportHour).Value));
    }

    public Result UpdateExportHour(TimeOnly newHour)
    {
        var hourResult = ExportHour.Create(newHour);
        if (hourResult.IsFailed)
            return Result.Fail(hourResult.Errors);

        ExportHour = hourResult.Value;
        return Result.Ok();
    }
}
