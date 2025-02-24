using FluentResults;
using ITS.BiblioAccess.Domain.Entities;
using ITS.BiblioAccess.Domain.ValueObjects;

namespace ITS.BiblioAccess.Domain.Repositories;

public interface IEntryRecordRepository
{
    Task<Result> AddAsync(EntryRecord entryRecord, CancellationToken ct);
}

public record EntryRecordCriteria
{
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public Guid? CareerId { get; init; }
    public UserType? UserType { get; init; }
    public Gender? Gender { get; init; }
    public bool GroupByCareer { get; init; }
    public bool GroupByUserType { get; init; }
}