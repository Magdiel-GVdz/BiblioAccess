using FluentResults;
using ITS.BiblioAccess.Domain.Entities;
using ITS.BiblioAccess.Domain.Repositories;

namespace ITS.BiblioAccess.Infrastructure.Data.Adapters;

public class EntryRecordRepository : IEntryRecordRepository
{
    public Task<Result> AddAsync(EntryRecord entryRecord, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
