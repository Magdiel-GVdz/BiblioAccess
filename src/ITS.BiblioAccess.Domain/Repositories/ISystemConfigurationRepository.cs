using FluentResults;
using ITS.BiblioAccess.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITS.BiblioAccess.Domain.Repositories;

public interface ISystemConfigurationRepository
{
    Task<Result<SystemConfiguration>> GetByKeyAsync(string key, CancellationToken cancellationToken);
    //Task<Result> AddAsync(SystemConfiguration config, CancellationToken cancellationToken);
    Task<Result> UpdateAsync(SystemConfiguration config, CancellationToken cancellationToken);
}