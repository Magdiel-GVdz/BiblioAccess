using FluentResults;
using ITS.BiblioAccess.Domain.Entities;
using ITS.BiblioAccess.Domain.Repositories;
using MediatR;

namespace ITS.BiblioAccess.Application.UseCases.Careers.Queries;

public class GetAllCareersUseCase
{
    public record GetAllCareersUseCaseQuery() : IRequest<Result<List<Career>>>;

    public class GetAllCareersUseCaseQueryHandler : IRequestHandler<GetAllCareersUseCaseQuery, Result<List<Career>>>
    {
        private readonly ICareerRepository _careerRepository;

        public GetAllCareersUseCaseQueryHandler(ICareerRepository careerRepository)
        {
            _careerRepository = careerRepository;
        }

        public async Task<Result<List<Career>>> Handle(GetAllCareersUseCaseQuery request, CancellationToken cancellationToken)
        {
            return await _careerRepository.GetAllAsync(cancellationToken);
        }
    }
}
