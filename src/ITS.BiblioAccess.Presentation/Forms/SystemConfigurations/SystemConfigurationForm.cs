using ITS.BiblioAccess.Application.UseCases.SystemConfigurations.Commands;
using ITS.BiblioAccess.Application.UseCases.SystemConfigurations.Queries;
using ITS.BiblioAccess.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ITS.BiblioAccess.Application.UseCases.SystemConfigurations.Commands.UpdateExportHourUseCase;

namespace ITS.BiblioAccess.Presentation.Forms.SystemConfigurations
{
    public partial class SystemConfigurationForm : Form
    {
        private readonly IMediator _mediator;
        public event Action<TimeOnly> OnExportHourUpdated;
        public SystemConfigurationForm(IMediator mediator)
        {
            _mediator = mediator;
            InitializeComponent();
        }

        private async void btnAccept_Click(object sender, EventArgs e)
        {
            // Obtener la nueva hora seleccionada por el usuario
            TimeOnly selectedTime = TimeOnly.FromDateTime(dtpExportHour.Value);

            // Ejecutar el caso de uso mediante MediatR
            var command = new UpdateExportHourCommand(selectedTime);
            var result = await _mediator.Send(command);

            // Verificar si la actualización fue exitosa
            if (result.IsSuccess)
            {
                MessageBox.Show("Hora de exportación actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                OnExportHourUpdated?.Invoke(selectedTime);
                this.Close();
            }
            else
            {
                MessageBox.Show("Error al actualizar la hora: " + string.Join("\n", result.Errors), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void SystemConfigurationForm_Load(object sender, EventArgs e)
        {
            var result = await _mediator.Send(new GetExportHourUseCase.GetExportHourQuery());

            if (result.IsSuccess)
            {
                dtpExportHour.Value = DateTime.Today.Add(result.Value.ToTimeSpan()); // Convierte TimeOnly a DateTime
            }
            else
            {
                MessageBox.Show("No se pudo obtener la hora de exportación. Se usará la hora actual por defecto.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpExportHour.Value = DateTime.Now;
            }
        }
    }
}
