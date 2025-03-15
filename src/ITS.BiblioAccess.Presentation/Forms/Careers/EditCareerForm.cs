using FluentResults;
using ITS.BiblioAccess.Domain.Entities;
using ITS.BiblioAccess.Presentation.Dtos;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;
using static ITS.BiblioAccess.Application.UseCases.Careers.Commands.EditCareerUseCase;

namespace ITS.BiblioAccess.Presentation.Forms.Careers
{
    public partial class EditCareerForm : Form
    {
        private readonly IMediator _mediator;
        private CareerDTO _career;

        public EditCareerForm(IMediator mediator)
        {
            _mediator = mediator;
            InitializeComponent();
        }

        public void SetCareer(CareerDTO career)
        {
            _career = career;
            txtName.Text = _career.Name;
            chkActive.Checked = _career.IsActive;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (_career == null)
            {
                MessageBox.Show("No hay una carrera seleccionada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var command = new UpdateCareerCommand(_career.Id, txtName.Text, chkActive.Checked);
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                MessageBox.Show("Carrera actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show(string.Join("\n", result.Errors), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
