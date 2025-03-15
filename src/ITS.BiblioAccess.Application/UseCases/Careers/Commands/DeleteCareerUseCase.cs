using FluentResults;
using ITS.BiblioAccess.Domain.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ITS.BiblioAccess.Application.UseCases.Careers.Commands;

public class DeleteCareerUseCase
{
    public record DeleteCareerCommand(Guid CareerId) : IRequest<Result>;

    public class DeleteCareerCommandHandler : IRequestHandler<DeleteCareerCommand, Result>
    {
        private readonly ICareerRepository _careerRepository;

        public DeleteCareerCommandHandler(ICareerRepository careerRepository)
        {
            _careerRepository = careerRepository;
        }

        public async Task<Result> Handle(DeleteCareerCommand request, CancellationToken cancellationToken)
        {
            return await _careerRepository.DeleteAsync(request.CareerId, cancellationToken);
        }
    }
}
