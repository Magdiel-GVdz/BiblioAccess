using FluentResults;

namespace ITS.BiblioAccess.Domain.ValueObjects;

public record CareerName
{
    public string Value { get; }

    private CareerName(string value) => Value = value;

    public static Result<CareerName> Create(string value)
    {
        var errors = new List<IError>();

        if (string.IsNullOrWhiteSpace(value))
        {
            errors.Add(new Error("Career name cannot be empty."));
        }
        else if (value.Length < 3)
        {
            errors.Add(new Error("Career name must be at least 3 characters long."));
        }
        else if (value.Length > 100)
        {
            errors.Add(new Error("Career name cannot exceed 100 characters."));
        }

        return errors.Count > 0 ? 
            Result.Fail(errors) : 
            Result.Ok(new CareerName(value));
    }

    public override string ToString() => Value;
}