using FluentResults;
using ITS.BiblioAccess.Domain.Entities;
using ITS.BiblioAccess.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ITS.BiblioAccess.Domain.Repositories;

public interface IEntryRecordRepository
{
    Task<Result> AddAsync(EntryRecord entryRecord, CancellationToken ct);
    Task<Result<(int MaleCount, int FemaleCount)>> GetDailyGenderCountAsync(DateTime date, CancellationToken ct);
    Task<Result<List<EntryRecord>>> GetRecordsByDateAsync(DateTime date, CancellationToken ct);
    Task<Result<List<EntryRecord>>> GetRecordsByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken ct);
    Task<Result<Dictionary<string, int>>> GetDailyEntriesByCareerAsync(DateTime date, CancellationToken ct);
    Task<Result<Dictionary<string, Dictionary<Gender, int>>>> GetDailyEntriesByCareerAndGenderAsync(DateTime date, CancellationToken ct);
}
