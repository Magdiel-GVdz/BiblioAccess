namespace ITS.BiblioAccess.Presentation.Forms.Reports;

public partial class ExcelReportsForm : Form
{
    private readonly ExcelExporter _excelExporter;

    public ExcelReportsForm(ExcelExporter excelExporter)
    {
        InitializeComponent();
        _excelExporter = excelExporter;
    }


    private void label3_Click(object sender, EventArgs e)
    {

    }

    private async void btnExportByDay_Click(object sender, EventArgs e)
    {
        try
        {
            bool success = await _excelExporter.ExportDailyEntriesByDate(dtpExportByDay.Value.Date);

            if (success)
            {
                MessageBox.Show("Exportación exitosa.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No se encontraron registros para el día seleccionado.", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ocurrió un error al exportar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnExportByDateRange_Click(object sender, EventArgs e)
    {
        try
        {
            var startDate = dtpExportStartDate.Value.Date;
            var endDate = dtpExportEndDay.Value.Date;

            if (startDate > endDate)
            {
                MessageBox.Show("La fecha de inicio no puede ser posterior a la fecha de fin.", "Rango inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool success = await _excelExporter.ExportDailyEntriesByDateRange(startDate, endDate);

            if (success)
            {
                MessageBox.Show("Exportación exitosa.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No se encontraron registros en el rango de fechas.", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ocurrió un error al exportar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

}
