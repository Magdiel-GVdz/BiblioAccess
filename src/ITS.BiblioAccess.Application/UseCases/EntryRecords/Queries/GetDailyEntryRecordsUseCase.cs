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
            // Obtener la zona horaria de Ciudad de México
            TimeZoneInfo mexicoTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/Mexico_City");

            // Convertir UTC a la hora de México y obtener solo la fecha (sin la hora)
            DateTime todayMexico = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, mexicoTimeZone).Date;

            return await _entryRecordRepository.GetRecordsByDateAsync(todayMexico, cancellationToken);
        }
    }
}
