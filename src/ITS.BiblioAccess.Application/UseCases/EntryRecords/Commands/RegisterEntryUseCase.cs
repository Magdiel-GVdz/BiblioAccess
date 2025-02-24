using FluentResults;
using ITS.BiblioAccess.Domain.Entities;
using ITS.BiblioAccess.Domain.Repositories;
using ITS.BiblioAccess.Domain.ValueObjects;
using MediatR;

namespace ITS.BiblioAccess.Application.UseCases.EntryRecords.Commands;

public class RegisterEntryUseCase
{
    public record RegisterEntryCommand(
            UserType UserType,
            Gender Gender,
            Guid? CareerId = null
        ) : IRequest<Result<Guid>>;

    public class RegisterEntryCommandHandler
        : IRequestHandler<RegisterEntryCommand, Result<Guid>>
    {
        private readonly ICareerRepository _careerRepository;
        private readonly IEntryRecordRepository _entryRecordRepository;

        public RegisterEntryCommandHandler(
            ICareerRepository careerRepository,
            IEntryRecordRepository entryRecordRepository)
        {
            _careerRepository = careerRepository;
            _entryRecordRepository = entryRecordRepository;
        }

        public async Task<Result<Guid>> Handle(RegisterEntryCommand request,
            CancellationToken cancellationToken)
        {
            // 1. If user is student, validate CareerId is present and career is active
            if (request.UserType == UserType.Student)
            {
                if (request.CareerId == null)
                {
                    return Result.Fail("CareerId is required for Student user type.");
                }

                var careerResult = await _careerRepository
                    .GetByIdAsync(request.CareerId.Value, cancellationToken);

                if (careerResult.IsFailed)
                {
                    return Result.Fail(careerResult.Errors);
                }

                var career = careerResult.Value;
                if (!career.IsActive)
                {
                    return Result.Fail($"Career {career.Name} is inactive.");
                }
            }

            // 2. Create EntryRecord (Domain logic might be encapsulated in a factory method)
            var recordResult = EntryRecord.Create(
                userType: request.UserType,
                gender: request.Gender,
                careerId: request.CareerId
            );
            if (recordResult.IsFailed)
            {
                return Result.Fail(recordResult.Errors);
            }

            var record = recordResult.Value;

            // 3. Persist EntryRecord
            var addResult = await _entryRecordRepository.AddAsync(record, cancellationToken);
            if (addResult.IsFailed)
            {
                return Result.Fail(addResult.Errors);
            }

            return Result.Ok(record.EntryId);
        }
    }
}

