using ClosedXML.Excel;
using ITS.BiblioAccess.Domain.Entities;
using ITS.BiblioAccess.Domain.Repositories;
using ITS.BiblioAccess.Domain.ValueObjects;
using MediatR;
using static ITS.BiblioAccess.Application.UseCases.EntryRecords.Queries.GetDailyEntryRecordsUseCase;
using static ITS.BiblioAccess.Application.UseCases.EntryRecords.Queries.GetRecordsByDateRangeUseCase;
using static ITS.BiblioAccess.Application.UseCases.EntryRecords.Queries.GetRecordsByDateUseCase;

public class ExcelExporter
{
    private readonly ICareerRepository _careerRepository;
    private readonly IMediator _mediator;

    public ExcelExporter(ICareerRepository careerRepository, IMediator mediator)
    {
        _careerRepository = careerRepository;
        _mediator = mediator;
    }

    public async Task<bool> ExportDailyEntriesToExcel()
    {
        var result = await _mediator.Send(new GetDailyEntryRecordsQuery());

        if (result.IsSuccess && result.Value.Count > 0)
        {
            string fileName = $"RegistrosEntrada_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.xlsx";
            await ExportEntriesToExcel(result.Value, fileName);
            return true;
        }
        return false;
    }

    public async Task<bool> ExportDailyEntriesByDate(DateTime date)
    {
        var result = await _mediator.Send(new GetRecordsByDateQuery(date));

        if (result.IsSuccess && result.Value.Count > 0)
        {
            string fileName = $"RegistrosEntrada_{date:yyyy-MM-dd}.xlsx";
            await ExportEntriesToExcel(result.Value, fileName);
            return true;
        }
        return false;
    }

    public async Task<bool> ExportDailyEntriesByDateRange(DateTime startDate, DateTime endDate)
    {
        var result = await _mediator.Send(new GetRecordsByDateRangeQuery(startDate, endDate));

        if (result.IsSuccess && result.Value.Count > 0)
        {
            string fileName = $"RegistrosEntrada_{startDate:yyyy-MM-dd}_a_{endDate:yyyy-MM-dd}.xlsx";
            await ExportEntriesToExcel(result.Value, fileName);
            return true;
        }
        return false;
    }

    private async Task ExportEntriesToExcel(List<EntryRecord> entries, string fileName)
    {
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Entradas");

            var careerNames = await GetCareerNames(entries);

            worksheet.Cell(1, 2).Value = "Tipo de Usuario";
            worksheet.Cell(1, 3).Value = "Género";
            worksheet.Cell(1, 4).Value = "Carrera";
            worksheet.Cell(1, 5).Value = "Fecha de Ingreso";

            for (int i = 0; i < entries.Count; i++)
            {
                var row = i + 2;
                worksheet.Cell(row, 2).Value = ((UserType)entries[i].UserType).GetEnumDescription();

                worksheet.Cell(row, 3).Value = ((Gender)entries[i].Gender).GetEnumDescription();
                worksheet.Cell(row, 4).Value = entries[i].CareerId.HasValue
                    ? careerNames.GetValueOrDefault(entries[i].CareerId!.Value, "N/A")
                    : "N/A";
                worksheet.Cell(row, 5).Value = entries[i].Timestamp.ToString("yyyy-MM-dd HH:mm:ss");
            }

            worksheet.Columns().AdjustToContents();

            string reportsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Reportes");
            if (!Directory.Exists(reportsFolder))
            {
                Directory.CreateDirectory(reportsFolder);
            }

            string filePath = Path.Combine(reportsFolder, fileName);
            workbook.SaveAs(filePath);
        }
    }

    private async Task<Dictionary<Guid, string>> GetCareerNames(List<EntryRecord> entries)
    {
        var careerIds = entries.Where(e => e.CareerId.HasValue)
                               .Select(e => e.CareerId!.Value)
                               .Distinct()
                               .ToList();

        var careers = await _careerRepository.GetByIdsAsync(careerIds);
        return careers.ToDictionary(c => c.CareerId, c => c.Name.Value);
    }
}
