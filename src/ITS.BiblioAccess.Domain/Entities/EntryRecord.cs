﻿using FluentResults;
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

    private EntryRecord() { }

    public static Result<EntryRecord> Create(UserType userType, Gender gender, Guid? careerId = null)
    {
        var errors = new List<IError>();

        if (userType == UserType.Student && careerId == null)
        {
            errors.Add(new Error("Students must have a career assigned."));
        }

        if (!Enum.IsDefined(userType))
        {
            errors.Add(new Error("Invalid user type."));
        }

        if (!Enum.IsDefined(gender))
        {
            errors.Add(new Error("Invalid gender type."));
        }

        if (errors.Count > 0)
        {
            return Result.Fail(errors);
        }


        TimeZoneInfo mexicoTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/Mexico_City");
        DateTime localMexicoTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, mexicoTimeZone);

        return Result.Ok(new EntryRecord(Guid.NewGuid(), localMexicoTime, userType, careerId, gender));
    }

}
