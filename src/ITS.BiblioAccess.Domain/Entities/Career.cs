﻿using FluentResults;
using ITS.BiblioAccess.Domain.ValueObjects;

namespace ITS.BiblioAccess.Domain.Entities;

public class Career
{
    public Guid CareerId { get; private init; }
    public CareerName Name { get; private set; }
    public bool IsActive { get; private set; }

    private Career() {}

    private Career(Guid careerId, CareerName careerName, bool isActive)
    {
        CareerId = careerId;
        Name = careerName;
        IsActive = isActive;
    }

    public static Result<Career> Create(Guid careerId, string careerName, bool isActive)
    {
        var errors = new List<IError>();

        if (careerId == Guid.Empty)
        {
            errors.Add(new Error("Career ID cannot be empty."));
        }

        var nameResult = CareerName.Create(careerName);
        if (nameResult.IsFailed)
        {
            errors.AddRange(nameResult.Errors);
        }

        return errors.Count > 0 ?
            Result.Fail(errors) :
            Result.Ok(new Career(careerId, nameResult.Value, isActive));
    }

    public static Result<Career> Create(string careerName)
    {
        return Create(Guid.NewGuid(), careerName, true);
    }

    public Result UpdateName(string newCareerName)
    {
        var nameResult = CareerName.Create(newCareerName);
        if (nameResult.IsFailed)
        {
            return Result.Fail(nameResult.Errors);
        }

        Name = nameResult.Value;
        return Result.Ok();
    }

    public void Deactivate() => IsActive = false;
    public void Activate() => IsActive = true;
}
