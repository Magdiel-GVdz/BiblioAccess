using FluentResults;
using ITS.BiblioAccess.Domain.Repositories;
using MediatR;

namespace ITS.BiblioAccess.Application.UseCases.Careers.Commands;

public class EditCareerUseCase
{
    public record UpdateCareerCommand(Guid CareerId, string NewName) : IRequest<Result>;

    public class UpdateCareerCommandHandler : IRequestHandler<UpdateCareerCommand, Result>
    {
        private readonly ICareerRepository _careerRepository;

        public UpdateCareerCommandHandler(ICareerRepository careerRepository)
        {
            _careerRepository = careerRepository;
        }

        public async Task<Result> Handle(UpdateCareerCommand request, CancellationToken cancellationToken)
        {
            var career = await _careerRepository.GetByIdAsync(request.CareerId, cancellationToken);
            if (career == null)
            {
                return Result.Fail("Career not found.");
            }

            var updateResult = career.Value.UpdateName(request.NewName);
            if (updateResult.IsFailed)
            {
                return Result.Fail(updateResult.Errors);
            }

            await _careerRepository.UpdateAsync(career.Value, cancellationToken);

            return Result.Ok();
        }
    }
}
