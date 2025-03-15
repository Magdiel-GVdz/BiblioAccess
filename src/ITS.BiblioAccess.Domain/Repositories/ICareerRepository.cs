using FluentResults;
using ITS.BiblioAccess.Domain.Entities;

namespace ITS.BiblioAccess.Domain.Repositories;

public interface ICareerRepository
{
    Task<Result<Career>> GetByIdAsync(Guid id, CancellationToken ct);
    Task<Result> AddAsync(Career career, CancellationToken ct);
    Task<Result> UpdateAsync(Career career, CancellationToken ct);
    Task<Result<List<Career>>> GetAllActiveAsync(CancellationToken ct);
    Task<Result<List<Career>>> GetAllAsync(CancellationToken ct);
    Task<Result> DeleteAsync(Guid id, CancellationToken ct);
}
