using FluentResults;
using ITS.BiblioAccess.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITS.BiblioAccess.Application.UseCases.EntryRecords.Queries;

public class GetDailyEntriesByCareerUseCase
{
    public record GetDailyEntriesByCareerQuery(DateTime Date) : IRequest<Result<Dictionary<string, int>>>;

    public class GetDailyEntriesByCareerQueryHandler : IRequestHandler<GetDailyEntriesByCareerQuery, Result<Dictionary<string, int>>>
    {
        private readonly IEntryRecordRepository _entryRecordRepository;

        public GetDailyEntriesByCareerQueryHandler(IEntryRecordRepository entryRecordRepository)
        {
            _entryRecordRepository = entryRecordRepository;
        }

        public async Task<Result<Dictionary<string, int>>> Handle(GetDailyEntriesByCareerQuery request, CancellationToken cancellationToken)
        {
            return await _entryRecordRepository.GetDailyEntriesByCareerAsync(request.Date, cancellationToken);
        }
    }
}
