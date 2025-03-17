using FluentResults;
using ITS.BiblioAccess.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITS.BiblioAccess.Application.UseCases.EntryRecords.Queries;

public class GetDailyGenderCountUseCase
{
    public record GetDailyGenderCountQuery(DateTime Date) : IRequest<Result<(int MaleCount, int FemaleCount)>>;

    public class GetDailyGenderCountQueryHandler : IRequestHandler<GetDailyGenderCountQuery, Result<(int MaleCount, int FemaleCount)>>
    {
        private readonly IEntryRecordRepository _entryRecordRepository;

        public GetDailyGenderCountQueryHandler(IEntryRecordRepository entryRecordRepository)
        {
            _entryRecordRepository = entryRecordRepository;
        }

        public async Task<Result<(int MaleCount, int FemaleCount)>> Handle(GetDailyGenderCountQuery request, CancellationToken cancellationToken)
        {
            return await _entryRecordRepository.GetDailyGenderCountAsync(request.Date, cancellationToken);
        }
    }
}