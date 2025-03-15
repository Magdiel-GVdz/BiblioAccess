using ITS.BiblioAccess.Domain.Entities;
using ITS.BiblioAccess.Presentation.Dtos;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var addCareerForm = _serviceProvider.GetRequiredService<AddCareerForm>())
            {
                if (addCareerForm.ShowDialog() == DialogResult.OK)
                {
                    Refresh();
                }
            }
        }

        private void CareersForm_Load(object sender, EventArgs e)
        {
            Refresh();
        }

        public override void Refresh()
        {
            var result = _mediator.Send(new GetAllCareersUseCaseQuery()).GetAwaiter().GetResult();

            if (result.IsSuccess)
            {
                List<Career> careers = result.Value;
                List<CareerDTO> careerDTOs = careers.Select(CareerDTO.FromCareer).ToList();
                dgvCareers.DataSource = careerDTOs;
                HideIdColumn();
            }
        }

        private void HideIdColumn()
        {
            if (dgvCareers.Columns["Id"] != null)
            {
                dgvCareers.Columns["Id"].Visible = false;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvCareers.CurrentRow != null)
            {
                CareerDTO selectedCareer = (CareerDTO)dgvCareers.CurrentRow.DataBoundItem;

                using (var editCareerForm = _serviceProvider.GetRequiredService<EditCareerForm>())
                {
                    editCareerForm.SetCareer(selectedCareer);

                    if (editCareerForm.ShowDialog() == DialogResult.OK)
                    {
                        Refresh();
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una carrera para editar.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCareers.CurrentRow != null)
            {
                CareerDTO selectedCareer = (CareerDTO)dgvCareers.CurrentRow.DataBoundItem;
                Guid careerId = selectedCareer.Id;

                using (var confirmDeleteForm = new ConfirmDeleteCareerform(_mediator, careerId))
                {
                    if (confirmDeleteForm.ShowDialog() == DialogResult.OK)
                    {
                        Refresh();
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una carrera para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
