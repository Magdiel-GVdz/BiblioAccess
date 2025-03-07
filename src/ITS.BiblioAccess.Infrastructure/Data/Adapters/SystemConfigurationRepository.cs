using FluentResults;
using ITS.BiblioAccess.Domain.Entities;
using ITS.BiblioAccess.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITS.BiblioAccess.Infrastructure.Data.Adapters;

public class SystemConfigurationRepository : ISystemConfigurationRepository
{
    public Task<Result<SystemConfiguration>> GetByKeyAsync(string key, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result> UpdateAsync(SystemConfiguration config, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
