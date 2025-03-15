using System;
using System.Windows.Forms;
using MediatR;
using ITS.BiblioAccess.Application.UseCases.Careers.Commands;
using FluentResults;

namespace ITS.BiblioAccess.Presentation.Forms.Careers
{
    public partial class AddCareerForm : Form
    {
        private readonly IMediator _mediator;

        public AddCareerForm(IMediator mediator)
        {
            _mediator = mediator;
            InitializeComponent();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            string careerName = txtName.Text.Trim();

            if (string.IsNullOrEmpty(careerName))
            {
                MessageBox.Show("El nombre de la carrera es obligatorio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var command = new CreateCareerUseCase.CreateCareerCommand(careerName);
            Result<Guid> result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                MessageBox.Show("Carrera añadida correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Cierra el formulario después de la inserción
            }
            else
            {
                MessageBox.Show($"Error: {string.Join(", ", result.Errors)}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddCareerForm_Load(object sender, EventArgs e)
        {

        }
    }
}
