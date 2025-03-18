using FluentResults;
using ITS.BiblioAccess.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ITS.BiblioAccess.Application.UseCases.EntryRecords.Queries
{
    public class GetDailyCareerCountUseCase
    {
        // Query que solicita los conteos de entradas por carrera para una fecha específica
        public record GetDailyCareerCountQuery()
            : IRequest<Result<List<(string CareerName, int Count)>>>;

        // Handler que maneja la consulta y llama al repositorio
        public class GetDailyCareerCountQueryHandler
            : IRequestHandler<GetDailyCareerCountQuery, Result<List<(string CareerName, int Count)>>>
        {
            private readonly IEntryRecordRepository _entryRecordRepository;

            public GetDailyCareerCountQueryHandler(IEntryRecordRepository entryRecordRepository)
            {
                _entryRecordRepository = entryRecordRepository;
            }

            public async Task<Result<List<(string CareerName, int Count)>>> Handle(
                GetDailyCareerCountQuery request,
                CancellationToken cancellationToken)
            {
                // ✅ Convertir la fecha UTC a la zona horaria de México
                TimeZoneInfo mexicoTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/Mexico_City");
                DateTime todayMexico = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, mexicoTimeZone).Date;

                // ✅ Llamar al repositorio con la fecha correcta
                var result = await _entryRecordRepository.GetDailyEntriesByCareerAsync(todayMexico, cancellationToken);

                if (result.IsSuccess)
                {
                    var careerCounts = result.Value
                        .Where(entry => entry.Key != "Unknown")
                        .Select(entry => (entry.Key, entry.Value))
                        .ToList();

                    return Result.Ok(careerCounts);
                }

                return Result.Fail(result.Errors);
            }
        }
    }
}
