using FluentResults;
using ITS.BiblioAccess.Domain.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ITS.BiblioAccess.Application.UseCases.EntryRecords.Queries;

public class GetDailyGenderCountUseCase
{
    public record GetDailyGenderCountQuery() : IRequest<Result<(int MaleCount, int FemaleCount)>>;

    public class GetDailyGenderCountQueryHandler : IRequestHandler<GetDailyGenderCountQuery, Result<(int MaleCount, int FemaleCount)>>
    {
        private readonly IEntryRecordRepository _entryRecordRepository;

        public GetDailyGenderCountQueryHandler(IEntryRecordRepository entryRecordRepository)
        {
            _entryRecordRepository = entryRecordRepository;
        }

        public async Task<Result<(int MaleCount, int FemaleCount)>> Handle(GetDailyGenderCountQuery request, CancellationToken cancellationToken)
        {
            // ✅ Convertir la fecha UTC a la zona horaria de México
            TimeZoneInfo mexicoTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/Mexico_City");
            DateTime todayMexico = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, mexicoTimeZone).Date;

            // ✅ Pasar la fecha correcta al repositorio
            return await _entryRecordRepository.GetDailyGenderCountAsync(todayMexico, cancellationToken);
        }
    }
}
