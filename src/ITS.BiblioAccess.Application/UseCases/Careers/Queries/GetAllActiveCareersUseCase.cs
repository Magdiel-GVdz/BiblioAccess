using FluentResults;
using ITS.BiblioAccess.Domain.Entities;
using ITS.BiblioAccess.Domain.Repositories;
using MediatR;

namespace ITS.BiblioAccess.Application.UseCases.Careers.Queries;

public class GetAllActiveCareersUseCase
{
    public record GetAllActiveCareersQuery() : IRequest<Result<List<Career>>>;

    public class GetAllActiveCareersQueryHandler : IRequestHandler<GetAllActiveCareersQuery, Result<List<Career>>>
    {
        private readonly ICareerRepository _careerRepository;

        public GetAllActiveCareersQueryHandler(ICareerRepository careerRepository)
        {
            _careerRepository = careerRepository;
        }

        public async Task<Result<List<Career>>> Handle(GetAllActiveCareersQuery request, CancellationToken cancellationToken)
        {
            return await _careerRepository.GetAllActiveAsync(cancellationToken);
        }
    }
}
