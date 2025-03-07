using FluentResults;
using ITS.BiblioAccess.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ITS.BiblioAccess.Domain.Repositories
{
    public interface ISystemConfigurationRepository
    {
        Task<Result<SystemConfiguration>> GetAsync(CancellationToken ct);
        Task<Result> SetAsync(SystemConfiguration configuration, CancellationToken ct);
    }
}
