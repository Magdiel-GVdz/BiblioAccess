using FluentResults;
using ITS.BiblioAccess.Domain.Entities;
using ITS.BiblioAccess.Domain.Repositories;
using MediatR;

namespace ITS.BiblioAccess.Application.UseCases.Careers.Commands;

public class CreateCareerUseCase
{
    public record CreateCareerCommand(string CareerName) : IRequest<Result<Guid>>;
    public class CreateCareerCommandHandler : IRequestHandler<CreateCareerCommand, Result<Guid>>
    {
        private readonly ICareerRepository _careerRepository;

        public CreateCareerCommandHandler(ICareerRepository careerRepository)
        {
            _careerRepository = careerRepository;
        }

        public async Task<Result<Guid>> Handle(CreateCareerCommand request, CancellationToken cancellationToken)
        {
            var careerResult = Career.Create(request.CareerName);
            if (careerResult.IsFailed)
            {
                return Result.Fail(careerResult.Errors);
            }

            var career = careerResult.Value;

            return await _careerRepository.AddAsync(career, cancellationToken);
        }
    }
}
