using FluentResults;
using ITS.BiblioAccess.Domain.ValueObjects;

namespace ITS.BiblioAccess.Domain.Entities;

public class EntryRecord
{
    public Guid EntryId { get; private init; }
    public DateTime Timestamp { get; private init; }
    public UserType UserType { get; private init; }
    public Guid? CareerId { get; private init; }
    public Gender Gender { get; private init; }

    private EntryRecord(Guid entryId, DateTime timestamp, UserType userType, Guid? careerId, Gender gender)
    {
        EntryId = entryId;
        Timestamp = timestamp;
        UserType = userType;
        CareerId = careerId;
        Gender = gender;
    }

    public static Result<EntryRecord> Create(UserType userType, Gender gender, Guid? careerId = null)
    {
        var errors = new List<IError>();

        if (userType == UserType.Student && careerId == null)
        {
            errors.Add(new Error("Students must have a career assigned."));
        }

        if (!Enum.IsDefined(typeof(UserType), userType))
        {
            errors.Add(new Error("Invalid user type."));
        }

        if (!Enum.IsDefined(typeof(Gender), gender))
        {
            errors.Add(new Error("Invalid gender type."));
        }

        return errors.Count > 0
            ? Result.Fail(errors)
            : Result.Ok(new EntryRecord(Guid.NewGuid(), DateTime.Now, userType, careerId, gender));
    }
}
