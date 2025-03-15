using MediatR;
using static ITS.BiblioAccess.Application.UseCases.Careers.Commands.DeleteCareerUseCase;

namespace ITS.BiblioAccess.Presentation.Forms.Careers
{
    public partial class ConfirmDeleteCareerform : Form
    {
        private readonly IMediator _mediator;
        private readonly Guid _careerId;

        public ConfirmDeleteCareerform(IMediator mediator, Guid careerId)
        {
            InitializeComponent();
            _mediator = mediator;
            _careerId = careerId;
        }

        private async void btnAccept_Click(object sender, EventArgs e)
        {
            var result = await _mediator.Send(new DeleteCareerCommand(_careerId));

            if (result.IsSuccess)
            {
                MessageBox.Show("Carrera eliminada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Error al eliminar la carrera: " + result.Errors[0].Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
