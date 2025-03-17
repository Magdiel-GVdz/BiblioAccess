using FluentResults;
using ITS.BiblioAccess.Domain.Entities;
using ITS.BiblioAccess.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITS.BiblioAccess.Application.UseCases.EntryRecords.Queries;
public class GetDailyEntryRecordsUseCase
{
    public record GetDailyEntryRecordsQuery() : IRequest<Result<List<EntryRecord>>>;

    public class GetDailyEntryRecordsQueryHandler : IRequestHandler<GetDailyEntryRecordsQuery, Result<List<EntryRecord>>>
    {
        private readonly IEntryRecordRepository _entryRecordRepository;

        public GetDailyEntryRecordsQueryHandler(IEntryRecordRepository entryRecordRepository)
        {
            _entryRecordRepository = entryRecordRepository;
        }

        public async Task<Result<List<EntryRecord>>> Handle(GetDailyEntryRecordsQuery request, CancellationToken cancellationToken)
        {
            DateTime today = DateTime.UtcNow.Date; // Asegura que se use la fecha del día actual en UTC
            return await _entryRecordRepository.GetRecordsByDateAsync(today, cancellationToken);
        }
    }
}
