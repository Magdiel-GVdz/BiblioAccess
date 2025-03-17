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

public class GetRecordsByDateRangeUseCase
{
    public record GetRecordsByDateRangeQuery(DateTime StartDate, DateTime EndDate) : IRequest<Result<List<EntryRecord>>>;

    public class GetRecordsByDateRangeQueryHandler : IRequestHandler<GetRecordsByDateRangeQuery, Result<List<EntryRecord>>>
    {
        private readonly IEntryRecordRepository _entryRecordRepository;

        public GetRecordsByDateRangeQueryHandler(IEntryRecordRepository entryRecordRepository)
        {
            _entryRecordRepository = entryRecordRepository;
        }

        public async Task<Result<List<EntryRecord>>> Handle(GetRecordsByDateRangeQuery request, CancellationToken cancellationToken)
        {
            return await _entryRecordRepository.GetRecordsByDateRangeAsync(request.StartDate, request.EndDate, cancellationToken);
        }
    }
}