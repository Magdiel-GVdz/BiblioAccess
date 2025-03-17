using ClosedXML.Excel;
using ITS.BiblioAccess.Domain.Entities;
using ITS.BiblioAccess.Domain.Repositories;
using ITS.BiblioAccess.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ITS.BiblioAccess.Presentation.Utils;

public class ExcelExporter
{
    private readonly ICareerRepository _careerRepository;

    public ExcelExporter(ICareerRepository careerRepository)
    {
        _careerRepository = careerRepository;
    }

    public async Task ExportEntriesToExcel(List<EntryRecord> entries)
    {
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Entradas");

            // Obtener nombres de carreras desde la base de datos
            var careerNames = await GetCareerNames(entries);

            // Encabezados
            worksheet.Cell(1, 2).Value = "Tipo de Usuario";
            worksheet.Cell(1, 3).Value = "Género";
            worksheet.Cell(1, 4).Value = "Carrera";
            worksheet.Cell(1, 5).Value = "Fecha de Ingreso";

            // Llenar datos
            for (int i = 0; i < entries.Count; i++)
            {
                var row = i + 2;
                worksheet.Cell(row, 2).Value = entries[i].UserType.GetEnumDescription();
                worksheet.Cell(row, 3).Value = entries[i].Gender.GetEnumDescription();

                // Obtener nombre de la carrera o "N/A"
                worksheet.Cell(row, 4).Value = entries[i].CareerId.HasValue
                    ? careerNames.GetValueOrDefault(entries[i].CareerId!.Value, "N/A")
                    : "N/A";

                worksheet.Cell(row, 5).Value = entries[i].Timestamp.ToString("yyyy-MM-dd HH:mm:ss");
            }

            // Autoajustar columnas para mejor visibilidad
            worksheet.Columns().AdjustToContents();

            // 📌 Crear carpeta "Reportes" en el Escritorio si no existe
            string reportsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Reportes");
            if (!Directory.Exists(reportsFolder))
            {
                Directory.CreateDirectory(reportsFolder);
            }

            // 📌 Generar nombre de archivo con fecha y hora
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string filePath = Path.Combine(reportsFolder, $"RegistrosEntrada_{timestamp}.xlsx");

            // 📌 Guardar archivo
            workbook.SaveAs(filePath);
        }
    }

    // 📌 Obtener nombres de carreras desde la base de datos
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
