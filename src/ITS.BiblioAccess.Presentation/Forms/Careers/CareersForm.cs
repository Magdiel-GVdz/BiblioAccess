using ITS.BiblioAccess.Domain.Entities;
using ITS.BiblioAccess.Presentation.Dtos;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ITS.BiblioAccess.Application.UseCases.Careers.Commands.DeleteCareerUseCase;
using static ITS.BiblioAccess.Application.UseCases.Careers.Queries.GetAllCareersUseCase;

namespace ITS.BiblioAccess.Presentation.Forms.Careers
{
    public partial class CareersForm : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMediator _mediator;

        public CareersForm(IServiceProvider serviceProvider, IMediator mediator)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _mediator = mediator;
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            using (var addCareerForm = _serviceProvider.GetRequiredService<AddCareerForm>())
            {
                if (addCareerForm.ShowDialog() == DialogResult.OK)
                {
                    await RefreshAsync();
                }
            }
        }

        private async void CareersForm_Load(object sender, EventArgs e)
        {
            await RefreshAsync();
        }

        public override void Refresh()
        {
            var result = _mediator.Send(new GetAllCareersUseCaseQuery()).GetAwaiter().GetResult();

            if (result.IsSuccess)
            {
                List<Career> careers = result.Value;
                List<CareerDTO> careerDTOs = careers.Select(CareerDTO.FromCareer).ToList();
                dgvCareers.DataSource = null;
                dgvCareers.DataSource = careerDTOs;
                HideIdColumn();
            }
        }

        public async Task RefreshAsync()
        {
            var result = await _mediator.Send(new GetAllCareersUseCaseQuery());

            if (result.IsSuccess)
            {
                List<Career> careers = result.Value;
                List<CareerDTO> careerDTOs = careers.Select(CareerDTO.FromCareer).ToList();
                dgvCareers.DataSource = null; // Limpiar primero para forzar la actualización
                dgvCareers.DataSource = careerDTOs;
                HideIdColumn();
                RenameColumns();
            }
        }
        private void RenameColumns()
        {
            if (dgvCareers.Columns["Name"] != null)
            {
                dgvCareers.Columns["Name"].HeaderText = "Nombre";
            }

            if (dgvCareers.Columns["IsActive"] != null)
            {
                dgvCareers.Columns["IsActive"].HeaderText = "Activo";
            }
        }


        private void HideIdColumn()
        {
            if (dgvCareers.Columns["Id"] != null)
            {
                dgvCareers.Columns["Id"].Visible = false;
            }
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvCareers.CurrentRow != null)
            {
                CareerDTO selectedCareer = (CareerDTO)dgvCareers.CurrentRow.DataBoundItem;

                using (var editCareerForm = _serviceProvider.GetRequiredService<EditCareerForm>())
                {
                    editCareerForm.SetCareer(selectedCareer);

                    if (editCareerForm.ShowDialog() == DialogResult.OK)
                    {
                        await RefreshAsync();
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una carrera para editar.");
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCareers.CurrentRow != null)
            {
                CareerDTO selectedCareer = (CareerDTO)dgvCareers.CurrentRow.DataBoundItem;
                Guid careerId = selectedCareer.Id;

                var confirmResult = MessageBox.Show(
                    "¿Está seguro de que desea eliminar esta carrera?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirmResult == DialogResult.Yes)
                {
                    var result = await _mediator.Send(new DeleteCareerCommand(careerId));

                    if (result.IsSuccess)
                    {
                        MessageBox.Show(
                            "Carrera eliminada exitosamente.",
                            "Éxito",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                        await RefreshAsync();
                    }
                    else
                    {
                        MessageBox.Show(
                            "Error al eliminar la carrera: " + result.Errors[0].Message,
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
            }
            else
            {
                MessageBox.Show(
                    "Por favor, seleccione una carrera para eliminar.",
                    "Advertencia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }
    }
}
