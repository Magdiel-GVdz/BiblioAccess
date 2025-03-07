using FluentResults;
using ITS.BiblioAccess.Domain.Entities;
using ITS.BiblioAccess.Domain.Repositories;
using MediatR;

namespace ITS.BiblioAccess.Application.UseCases.SystemConfigurations.Commands;

public class UpdateExportHourUseCase
{
    public record UpdateExportHourCommand(TimeOnly NewHour) : IRequest<Result>;

    public class UpdateExportHourCommandHandler : IRequestHandler<UpdateExportHourCommand, Result>
    {
        private readonly ISystemConfigurationRepository _configRepository;

        public UpdateExportHourCommandHandler(ISystemConfigurationRepository configRepository)
        {
            _configRepository = configRepository;
        }

        public async Task<Result> Handle(UpdateExportHourCommand request, CancellationToken cancellationToken)
        {
            // Obtener la configuración existente o crear una nueva si no hay ninguna
            var existingConfigResult = await _configRepository.GetAsync(cancellationToken);
            SystemConfiguration config;

            if (existingConfigResult.IsSuccess)
            {
                config = existingConfigResult.Value;
                var updateResult = config.UpdateExportHour(request.NewHour);
                if (updateResult.IsFailed)
                    return Result.Fail(updateResult.Errors);
            }
            else
            {
                var createResult = SystemConfiguration.Create(request.NewHour);
                if (createResult.IsFailed)
                    return Result.Fail(createResult.Errors);

                config = createResult.Value;
            }

            // Guardar la configuración (ya sea nueva o actualizada)
            return await _configRepository.SetAsync(config, cancellationToken);
        }
    }
}
