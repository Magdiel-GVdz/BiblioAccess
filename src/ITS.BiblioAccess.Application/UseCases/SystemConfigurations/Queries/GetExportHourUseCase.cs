using FluentResults;
using ITS.BiblioAccess.Domain.Entities;
using ITS.BiblioAccess.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITS.BiblioAccess.Application.UseCases.SystemConfigurations.Queries;

public class GetExportHourUseCase
{
    public record GetExportHourQuery() : IRequest<Result<TimeOnly>>;

    public class GetExportHourQueryHandler : IRequestHandler<GetExportHourQuery, Result<TimeOnly>>
    {
        private readonly ISystemConfigurationRepository _configRepository;

        public GetExportHourQueryHandler(ISystemConfigurationRepository configRepository)
        {
            _configRepository = configRepository;
        }

        public async Task<Result<TimeOnly>> Handle(GetExportHourQuery request, CancellationToken cancellationToken)
        {
            Result<SystemConfiguration> existingConfigResult = await _configRepository.GetAsync(cancellationToken);

            if (existingConfigResult.IsSuccess)
            {
                return Result.Ok(existingConfigResult.Value.ExportHour.Value);
            }

            return Result.Fail("No se encontró una configuración de exportación.");
        }
    }
}