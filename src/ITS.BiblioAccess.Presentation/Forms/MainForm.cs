using ITS.BiblioAccess.Domain.Entities;
using ITS.BiblioAccess.Domain.Repositories;
using ITS.BiblioAccess.Presentation.Forms.Careers;
using ITS.BiblioAccess.Presentation.Forms.EntryRecors;
using ITS.BiblioAccess.Presentation.Forms.SystemConfigurations;
using ITS.BiblioAccess.Presentation.Utils;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Timers;
using static ITS.BiblioAccess.Application.UseCases.EntryRecords.Queries.GetDailyEntryRecordsUseCase;
using static ITS.BiblioAccess.Application.UseCases.SystemConfigurations.Queries.GetExportHourUseCase;

namespace ITS.BiblioAccess.Presentation.Forms
{
    public partial class MainForm : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMediator _mediator;
        private readonly ICareerRepository _careerRepository;
        private readonly System.Timers.Timer _timer;
        private TimeOnly _exportTime;

        public MainForm(IServiceProvider serviceProvider, IMediator mediator, ICareerRepository careerRepository)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _mediator = mediator;
            _careerRepository = careerRepository;
            

            // Configurar el Timer para ejecución programada
            _timer = new System.Timers.Timer(60000); // Revisión cada 60 segundos
            _timer.Elapsed += CheckScheduledExport;
            _timer.AutoReset = true;
            _timer.Start();

            LoadExportTime();
        }

        private async void LoadExportTime()
        {
            var result = await _mediator.Send(new GetExportHourQuery());

            if (result.IsSuccess)
            {
                _exportTime = result.Value;
            }
            else
            {
                MessageBox.Show("Error al cargar la hora de exportación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void CheckScheduledExport(object sender, ElapsedEventArgs e)
        {
            TimeSpan now = DateTime.Now.TimeOfDay;

            if (now.Hours == _exportTime.Hour && now.Minutes == _exportTime.Minute)
            {
                _timer.Stop(); // Evitar ejecuciones duplicadas
                await ExportEntries();
                _timer.Start();
            }
        }

        private async Task ExportEntries()
        {
            var result = await _mediator.Send(new GetDailyEntryRecordsQuery());

            if (result.IsSuccess && result.Value.Count > 0)
            {
                List<EntryRecord> entries = result.Value;

                try
                {
                    var excelExporter = new ExcelExporter(_careerRepository);
                    await excelExporter.ExportEntriesToExcel(entries);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al exportar registros: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCareers_Click(object sender, EventArgs e)
        {
            using var careersForm = _serviceProvider.GetRequiredService<CareersForm>();
            careersForm.ShowDialog();
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            using var systemConfigurationForm = _serviceProvider.GetRequiredService<SystemConfigurationForm>();

            // Asegurar que no se suscriba múltiples veces
            systemConfigurationForm.OnExportHourUpdated -= UpdateExportHour;
            systemConfigurationForm.OnExportHourUpdated += UpdateExportHour;

            systemConfigurationForm.ShowDialog();
        }

        private void UpdateExportHour(TimeOnly newExportHour)
        {
            _exportTime = newExportHour;
            MessageBox.Show($"La hora de exportación se ha actualizado a: {_exportTime}", "Actualización", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnEntryRecords_Click(object sender, EventArgs e)
        {
            using var entryRecordForm = _serviceProvider.GetRequiredService<EntryRecordForm>();
            entryRecordForm.ShowDialog();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("¿Seguro que quieres salir?", "Confirmar salida",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                e.Cancel = true; // Cancelar cierre
                return;
            }

            _timer.Stop();
            _timer.Dispose();
            System.Windows.Forms.Application.Exit();
        }
    }
}
