using FluentResults;
using ITS.BiblioAccess.Domain.Entities;
using ITS.BiblioAccess.Domain.Repositories;
using MediatR;

namespace ITS.BiblioAccess.Application.UseCases.Careers.Commands;

public class ChangeActiveCareerStatus
{
    public record ChangeActiveCareerStatusCommand(Guid CareerId, bool IsDelete) : IRequest<Result<Career>>;

    public class ChangeActiveCareerStatusCommandHandler : IRequestHandler<ChangeActiveCareerStatusCommand, Result<Career>>
    {
        private readonly ICareerRepository _careerRepository;

        public ChangeActiveCareerStatusCommandHandler(ICareerRepository careerRepository)
        {
            _careerRepository = careerRepository;
        }

        public async Task<Result<Career>> Handle(ChangeActiveCareerStatusCommand request, CancellationToken cancellationToken)
        {
            Result<Career> career = await _careerRepository.GetByIdAsync(request.CareerId, cancellationToken);
            if (career.IsFailed)
            {
                return career;
            }
            if (request.IsDelete)
            {
                career.Value.Deactivate();
            }
            else
            {
                career.Value.Activate();
            }

            await _careerRepository.UpdateAsync(career.Value, cancellationToken);

            return Result.Ok();
        }
    }
}
