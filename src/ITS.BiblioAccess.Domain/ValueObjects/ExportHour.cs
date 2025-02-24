using FluentResults;

namespace ITS.BiblioAccess.Domain.ValueObjects;

public record ExportHour
{
    public TimeOnly Value { get; }

    private ExportHour(TimeOnly value)
    {
        Value = value;
    }

    public static Result<ExportHour> Create(TimeOnly hourString)
    {
        return Result.Ok(new ExportHour(hourString));
    }

    public override string ToString()
    {
        return Value.ToString(@"hh\:mm");
    }
}
