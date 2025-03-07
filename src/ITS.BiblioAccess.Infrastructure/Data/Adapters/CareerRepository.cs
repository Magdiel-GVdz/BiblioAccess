using FluentResults;
using ITS.BiblioAccess.Domain.Entities;
using ITS.BiblioAccess.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ITS.BiblioAccess.Infrastructure.Data.Adapters;

public class CareerRepository : ICareerRepository
{
    private readonly AppDBContext _context;

    public CareerRepository(AppDBContext context)
    {
        _context = context;
    }

    public async Task<Result> AddAsync(Career career, CancellationToken ct)
    {
        try
        {
            await _context.Careers.AddAsync(career, ct);
            await _context.SaveChangesAsync(ct);
            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail($"Error adding career: {ex.Message}");
        }
    }

    public async Task<Result<List<Career>>> GetAllActiveAsync(CancellationToken ct)
    {
        try
        {
            var careers = await _context.Careers
                .Where(c => c.IsActive)
                .ToListAsync(ct);
            return Result.Ok(careers);
        }
        catch (Exception ex)
        {
            return Result.Fail($"Error retrieving active careers: {ex.Message}");
        }
    }

    public async Task<Result<List<Career>>> GetAllAsync(CancellationToken ct)
    {
        try
        {
            var careers = await _context.Careers.ToListAsync(ct);
            return Result.Ok(careers);
        }
        catch (Exception ex)
        {
            return Result.Fail($"Error retrieving careers: {ex.Message}");
        }
    }

    public async Task<Result<Career>> GetByIdAsync(Guid id, CancellationToken ct)
    {
        try
        {
            var career = await _context.Careers.FindAsync(new object[] { id }, ct);
            return career is not null ? Result.Ok(career) : Result.Fail("Career not found.");
        }
        catch (Exception ex)
        {
            return Result.Fail($"Error retrieving career by ID: {ex.Message}");
        }
    }

    public async Task<Result> UpdateAsync(Career career, CancellationToken ct)
    {
        try
        {
            var existingCareer = await _context.Careers.FindAsync(new object[] { career.CareerId }, ct);
            if (existingCareer is null)
                return Result.Fail("Career not found.");

            _context.Entry(existingCareer).CurrentValues.SetValues(career);
            await _context.SaveChangesAsync(ct);
            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail($"Error updating career: {ex.Message}");
        }
    }
}
