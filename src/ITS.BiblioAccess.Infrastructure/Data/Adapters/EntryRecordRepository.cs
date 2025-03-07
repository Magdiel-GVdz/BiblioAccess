using FluentResults;
using ITS.BiblioAccess.Domain.Entities;
using ITS.BiblioAccess.Domain.Repositories;
using ITS.BiblioAccess.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ITS.BiblioAccess.Infrastructure.Data.Adapters
{
    public class EntryRecordRepository : IEntryRecordRepository
    {
        private readonly AppDBContext _context;

        public EntryRecordRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Result> AddAsync(EntryRecord entryRecord, CancellationToken ct)
        {
            try
            {
                await _context.EntryRecords.AddAsync(entryRecord, ct);
                await _context.SaveChangesAsync(ct);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail($"Error adding entry record: {ex.Message}");
            }
        }

        public async Task<Result<(int MaleCount, int FemaleCount)>> GetDailyGenderCountAsync(DateTime date, CancellationToken ct)
        {
            try
            {
                var startDate = date.Date;
                var endDate = startDate.AddDays(1);

                var counts = await _context.EntryRecords
                    .Where(e => e.Timestamp >= startDate && e.Timestamp < endDate)
                    .GroupBy(e => e.Gender)
                    .Select(g => new { Gender = g.Key, Count = g.Count() })
                    .ToListAsync(ct);

                int maleCount = counts.FirstOrDefault(g => g.Gender == Gender.Male)?.Count ?? 0;
                int femaleCount = counts.FirstOrDefault(g => g.Gender == Gender.Female)?.Count ?? 0;

                return Result.Ok((maleCount, femaleCount));
            }
            catch (Exception ex)
            {
                return Result.Fail($"Error retrieving gender count: {ex.Message}");
            }
        }

        public async Task<Result<List<EntryRecord>>> GetRecordsByDateAsync(DateTime date, CancellationToken ct)
        {
            try
            {
                var startDate = date.Date;
                var endDate = startDate.AddDays(1);

                var records = await _context.EntryRecords
                    .Where(e => e.Timestamp >= startDate && e.Timestamp < endDate)
                    .ToListAsync(ct);

                return Result.Ok(records);
            }
            catch (Exception ex)
            {
                return Result.Fail($"Error retrieving records by date: {ex.Message}");
            }
        }

        public async Task<Result<List<EntryRecord>>> GetRecordsByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken ct)
        {
            try
            {
                var records = await _context.EntryRecords
                    .Where(e => e.Timestamp >= startDate.Date && e.Timestamp < endDate.Date.AddDays(1))
                    .ToListAsync(ct);

                return Result.Ok(records);
            }
            catch (Exception ex)
            {
                return Result.Fail($"Error retrieving records by date range: {ex.Message}");
            }
        }

        public async Task<Result<Dictionary<string, int>>> GetDailyEntriesByCareerAsync(DateTime date, CancellationToken ct)
        {
            try
            {
                var startDate = date.Date;
                var endDate = startDate.AddDays(1);

                var entriesByCareer = await _context.EntryRecords
                    .Where(e => e.Timestamp >= startDate && e.Timestamp < endDate && e.CareerId.HasValue)
                    .GroupBy(e => e.CareerId)
                    .Select(g => new
                    {
                        CareerId = g.Key,
                        Count = g.Count()
                    })
                    .ToListAsync(ct);

                var careerNames = await _context.Careers
                    .Where(c => entriesByCareer.Select(e => e.CareerId).Contains(c.CareerId))
                    .ToDictionaryAsync(c => c.CareerId, c => c.Name.Value, ct);

                var result = entriesByCareer
                    .ToDictionary(e => careerNames.ContainsKey(e.CareerId.Value) ? careerNames[e.CareerId.Value] : "Unknown", e => e.Count);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail($"Error retrieving daily entries by career: {ex.Message}");
            }
        }

        public async Task<Result<Dictionary<string, Dictionary<Gender, int>>>> GetDailyEntriesByCareerAndGenderAsync(DateTime date, CancellationToken ct)
        {
            try
            {
                var startDate = date.Date;
                var endDate = startDate.AddDays(1);

                var entriesByCareerGender = await _context.EntryRecords
                    .Where(e => e.Timestamp >= startDate && e.Timestamp < endDate && e.CareerId.HasValue)
                    .GroupBy(e => new { e.CareerId, e.Gender })
                    .Select(g => new
                    {
                        CareerId = g.Key.CareerId,
                        Gender = g.Key.Gender,
                        Count = g.Count()
                    })
                    .ToListAsync(ct);

                var careerNames = await _context.Careers
                    .Where(c => entriesByCareerGender.Select(e => e.CareerId).Contains(c.CareerId))
                    .ToDictionaryAsync(c => c.CareerId, c => c.Name.Value, ct);

                var result = new Dictionary<string, Dictionary<Gender, int>>();

                foreach (var entry in entriesByCareerGender)
                {
                    var careerName = careerNames.ContainsKey(entry.CareerId.Value) ? careerNames[entry.CareerId.Value] : "Unknown";

                    if (!result.ContainsKey(careerName))
                    {
                        result[careerName] = new Dictionary<Gender, int>
                        {
                            { Gender.Male, 0 },
                            { Gender.Female, 0 },
                            { Gender.NA, 0 }
                        };
                    }

                    result[careerName][entry.Gender] = entry.Count;
                }

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail($"Error retrieving daily entries by career and gender: {ex.Message}");
            }
        }
    }
}
