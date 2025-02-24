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

            // Buscar la configuración actual
            var existingConfigResult = await _configRepository.GetByKeyAsync("ExportHour", cancellationToken);
            if (existingConfigResult.IsFailed)
                existingConfigResult = SystemConfiguration.Create(request.NewHour);

            var config = existingConfigResult.Value;

            // Actualizar el valor
            var updateResult = config.UpdateExportHour(request.NewHour);
            if (updateResult.IsFailed)
                return Result.Fail(updateResult.Errors);

            // Guardar la nueva configuración en la BD
            return await _configRepository.UpdateAsync(config, cancellationToken);
        }
    }
}
