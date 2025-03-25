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

public class GetRecordsByDateUseCase
{
    public record GetRecordsByDateQuery(DateTime Date) : IRequest<Result<List<EntryRecord>>>;

    public class GetRecordsByDateQueryHandler : IRequestHandler<GetRecordsByDateQuery, Result<List<EntryRecord>>>
    {
        private readonly IEntryRecordRepository _entryRecordRepository;

        public GetRecordsByDateQueryHandler(IEntryRecordRepository entryRecordRepository)
        {
            _entryRecordRepository = entryRecordRepository;
        }

        public async Task<Result<List<EntryRecord>>> Handle(GetRecordsByDateQuery request, CancellationToken cancellationToken)
        {
            // Obtener la zona horaria de Ciudad de México
            TimeZoneInfo mexicoTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/Mexico_City");

            // Convertir la fecha proporcionada a la zona horaria de México y extraer solo la parte de la fecha
            DateTime targetDateInMexico = TimeZoneInfo.ConvertTime(request.Date.Date, mexicoTimeZone);

            return await _entryRecordRepository.GetRecordsByDateAsync(targetDateInMexico, cancellationToken);
        }
    }
}