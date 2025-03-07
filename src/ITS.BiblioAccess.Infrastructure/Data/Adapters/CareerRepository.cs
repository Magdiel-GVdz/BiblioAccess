using FluentResults;
using ITS.BiblioAccess.Domain.Entities;
using ITS.BiblioAccess.Domain.Repositories;

namespace ITS.BiblioAccess.Infrastructure.Data.Adapters;

public class CareerRepository : ICareerRepository
{
    public Task<Result> AddAsync(Career career, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Result<List<Career>>> GetAllActiveAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Result<List<Career>>> GetAllAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Career>> GetByIdAsync(Guid id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Result> UpdateAsync(Career career, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
