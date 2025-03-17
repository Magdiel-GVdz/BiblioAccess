using FluentResults;
using ITS.BiblioAccess.Domain.Repositories;
using ITS.BiblioAccess.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITS.BiblioAccess.Application.UseCases.EntryRecords.Queries;

public class GetDailyEntriesByCareerAndGenderUseCase
{
    public record GetDailyEntriesByCareerAndGenderQuery(DateTime Date) : IRequest<Result<Dictionary<string, Dictionary<Gender, int>>>>;

    public class GetDailyEntriesByCareerAndGenderQueryHandler : IRequestHandler<GetDailyEntriesByCareerAndGenderQuery, Result<Dictionary<string, Dictionary<Gender, int>>>>
    {
        private readonly IEntryRecordRepository _entryRecordRepository;

        public GetDailyEntriesByCareerAndGenderQueryHandler(IEntryRecordRepository entryRecordRepository)
        {
            _entryRecordRepository = entryRecordRepository;
        }

        public async Task<Result<Dictionary<string, Dictionary<Gender, int>>>> Handle(GetDailyEntriesByCareerAndGenderQuery request, CancellationToken cancellationToken)
        {
            return await _entryRecordRepository.GetDailyEntriesByCareerAndGenderAsync(request.Date, cancellationToken);
        }
    }
}
