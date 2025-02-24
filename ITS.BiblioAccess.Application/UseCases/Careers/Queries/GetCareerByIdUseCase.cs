using FluentResults;
using ITS.BiblioAccess.Domain.Entities;
using ITS.BiblioAccess.Domain.Repositories;
using MediatR;

namespace ITS.BiblioAccess.Application.UseCases.Careers.Queries;

public class GetCareerByIdUseCase
{
    public record GetCareerByIdQuery(Guid CareerId) : IRequest<Result<Career>>;

    public class GetCareerByIdQueryHandler : IRequestHandler<GetCareerByIdQuery, Result<Career>>
    {
        private readonly ICareerRepository _careerRepository;

        public GetCareerByIdQueryHandler(ICareerRepository careerRepository)
        {
            _careerRepository = careerRepository;
        }

        public async Task<Result<Career>> Handle(GetCareerByIdQuery request, CancellationToken cancellationToken)
        {
            return await _careerRepository.GetByIdAsync(request.CareerId, cancellationToken);
        }
    }
}
