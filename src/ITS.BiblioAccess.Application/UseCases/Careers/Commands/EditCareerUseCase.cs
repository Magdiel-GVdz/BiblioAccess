using FluentResults;
using ITS.BiblioAccess.Domain.Repositories;
using MediatR;

namespace ITS.BiblioAccess.Application.UseCases.Careers.Commands;

public class EditCareerUseCase
{
    public sealed record UpdateCareerCommand(Guid CareerId, string? NewName, bool? IsActive) : IRequest<Result>;

    public sealed class UpdateCareerCommandHandler : IRequestHandler<UpdateCareerCommand, Result>
    {
        private readonly ICareerRepository _careerRepository;

        public UpdateCareerCommandHandler(ICareerRepository careerRepository)
        {
            _careerRepository = careerRepository;
        }

        public async Task<Result> Handle(UpdateCareerCommand request, CancellationToken cancellationToken)
        {
            var career = await _careerRepository.GetByIdAsync(request.CareerId, cancellationToken);
            if (career.IsFailed)
            {
                return Result.Fail("Career not found.");
            }

            if (!string.IsNullOrWhiteSpace(request.NewName))
            {
                var updateResult = career.Value.UpdateName(request.NewName);
                if (updateResult.IsFailed)
                {
                    return Result.Fail(updateResult.Errors);
                }
            }

            if (request.IsActive.HasValue)
            {
                if (request.IsActive.Value)
                {
                    career.Value.Activate();
                }
                else
                {
                    career.Value.Deactivate();
                }
            }

            await _careerRepository.UpdateAsync(career.Value, cancellationToken);

            return Result.Ok();
        }
    }
}
