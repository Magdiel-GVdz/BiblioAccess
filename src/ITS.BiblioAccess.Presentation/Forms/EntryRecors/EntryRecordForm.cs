using ITS.BiblioAccess.Application.UseCases.EntryRecords.Queries;
using ITS.BiblioAccess.Domain.Entities;
using ITS.BiblioAccess.Domain.ValueObjects;
using MediatR;
using System.Runtime.Versioning;
using static ITS.BiblioAccess.Application.UseCases.Careers.Queries.GetAllActiveCareersUseCase;
using static ITS.BiblioAccess.Application.UseCases.EntryRecords.Commands.RegisterEntryUseCase;

namespace ITS.BiblioAccess.Presentation.Forms.EntryRecors;

[SupportedOSPlatform("windows")]
public partial class EntryRecordForm : Form
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IMediator _mediator;

    private Label lblMaleCounter;
    private Label lblFemaleCounter;

    public EntryRecordForm(IServiceProvider serviceProvider, IMediator mediator)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
        _mediator = mediator;

        pnlButtons.Dock = DockStyle.None; // Evita que se estire
        pnlButtons.Anchor = AnchorStyles.None; // Permite centrarlo
        pnlButtons.AutoSize = true;
        pnlButtons.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        pnlButtons.Location = new System.Drawing.Point(
            (this.ClientSize.Width - pnlButtons.Width) / 2,
            (this.ClientSize.Height - pnlButtons.Height) / 2
        );
    }


    private void EntryRecordForm_Load(object sender, EventArgs e)
    {
        RenderCareerButtons();
    }

    private async void RenderCareerButtons()
    {
        pnlButtons.Controls.Clear(); // Limpia el contenido anterior
        pnlButtons.ColumnCount = 3;
        pnlButtons.RowCount = 1;
        pnlButtons.AutoSize = true;
        pnlButtons.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        pnlButtons.Dock = DockStyle.Top;
        pnlButtons.ColumnStyles.Clear();
        pnlButtons.RowStyles.Clear();

        // Definir las tres columnas con el mismo tamaño
        pnlButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
        pnlButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
        pnlButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));

        // 📌 Agregar título en la cima
        Label lblTitle = new Label
        {
            Text = "Registra tu entrada",
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font("Arial", 16, FontStyle.Bold),
            Dock = DockStyle.Top,
            AutoSize = true,
            Margin = new Padding(0, 10, 0, 20)
        };
        pnlButtons.Controls.Add(lblTitle, 0, 0);
        pnlButtons.SetColumnSpan(lblTitle, 3);

        // 📌 Contenedor para los contadores (centrado correctamente)
        TableLayoutPanel genderCounterPanel = new TableLayoutPanel
        {
            ColumnCount = 2,
            RowCount = 1,
            AutoSize = true,
            Anchor = AnchorStyles.None,
            Dock = DockStyle.None,  // 🚀 Evita que se expanda de más
            Padding = new Padding(0, 10, 0, 20),
            Margin = new Padding(0, 0, 0, 10)
        };

        // Definir las dos columnas con igual tamaño
        genderCounterPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        genderCounterPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

        // 📌 Contadores de ingresos por género
        lblMaleCounter = new Label
        {
            Text = "Ingresos de hombres: 0",
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font("Arial", 12, FontStyle.Bold),
            AutoSize = true,
            Anchor = AnchorStyles.None
        };

        lblFemaleCounter = new Label
        {
            Text = "Ingresos de mujeres: 0",
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font("Arial", 12, FontStyle.Bold),
            AutoSize = true,
            Anchor = AnchorStyles.None
        };

        // 📌 Agregar los labels al `TableLayoutPanel`
        genderCounterPanel.Controls.Add(lblMaleCounter, 0, 0);
        genderCounterPanel.Controls.Add(lblFemaleCounter, 1, 0);

        // 📌 Ubicar el panel en el centro de la pantalla
        genderCounterPanel.Location = new System.Drawing.Point(
            (this.ClientSize.Width - genderCounterPanel.Width) / 2,
            50 // Puedes ajustar este valor para moverlo más arriba o abajo
        );

        // 📌 Agregar el contenedor de los contadores al panel principal
        pnlButtons.Controls.Add(genderCounterPanel, 0, 1);
        pnlButtons.SetColumnSpan(genderCounterPanel, 3);


        await UpdateEntryCounter();

        var result = await _mediator.Send(new GetAllActiveCareersQuery());

        if (!result.IsSuccess)
        {
            MessageBox.Show("Error al obtener las carreras.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        List<Career> careers = result.Value;
        int rowIndex = 2;

        for (int i = 0; i < careers.Count; i += 3)
        {
            pnlButtons.RowCount++;
            pnlButtons.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            for (int j = 0; j < 3; j++)
            {
                if (i + j >= careers.Count) break;

                Career career = careers[i + j];

                var panelContainer = new TableLayoutPanel
                {
                    ColumnCount = 1,
                    RowCount = 2,
                    AutoSize = true,
                    Anchor = AnchorStyles.None,
                    Dock = DockStyle.Fill,
                    Padding = new Padding(10),
                    Margin = new Padding(10)
                };

                panelContainer.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                panelContainer.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                Label lblCareer = new Label
                {
                    Text = career.Name.Value,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Top,
                    Font = new Font("Arial", 12, FontStyle.Bold),
                    AutoSize = true
                };

                Button btnMale = new Button
                {
                    Text = "Hombre",
                    Tag = Tuple.Create(career.CareerId, "Male"),
                    Width = 150,
                    Height = 40
                };
                btnMale.Click += RegisterButton_Click;

                Button btnFemale = new Button
                {
                    Text = "Mujer",
                    Tag = Tuple.Create(career.CareerId, "Female"),
                    Width = 150,
                    Height = 40
                };
                btnFemale.Click += RegisterButton_Click;

                FlowLayoutPanel careerButtonPanel = new FlowLayoutPanel
                {
                    FlowDirection = FlowDirection.LeftToRight,
                    Dock = DockStyle.Fill,
                    AutoSize = true,
                    Anchor = AnchorStyles.None
                };

                careerButtonPanel.Controls.Add(btnMale);
                careerButtonPanel.Controls.Add(btnFemale);

                panelContainer.Controls.Add(lblCareer, 0, 0);
                panelContainer.Controls.Add(careerButtonPanel, 0, 1);

                pnlButtons.Controls.Add(panelContainer, j, rowIndex);
            }

            rowIndex++;
        }

        TableLayoutPanel externalTablePanel = new TableLayoutPanel
        {
            ColumnCount = 1,
            RowCount = 2,
            AutoSize = true,
            Anchor = AnchorStyles.None,
            Location = new Point(
            (this.ClientSize.Width - 500) / 2,
            pnlButtons.Bottom + 50
        ),
            Padding = new Padding(0, 10, 0, 0),
            BackColor = Color.Transparent
        };

        Label lblOtros = new Label
        {
            Text = "Otros",
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font("Arial", 12, FontStyle.Bold),
            AutoSize = true,
            Dock = DockStyle.Fill,
            Margin = new Padding(0, 0, 0, 10)
        };

        FlowLayoutPanel buttonPanel = new FlowLayoutPanel
        {
            FlowDirection = FlowDirection.LeftToRight,
            AutoSize = true,
            Anchor = AnchorStyles.None
        };

        Button btnDocente = new Button
        {
            Text = "Docente",
            Width = 150,
            Height = 40
        };
        btnDocente.Click += (s, e) => RegisterGeneralEntry(UserType.Docent);

        Button btnAdmin = new Button
        {
            Text = "Administrativo",
            Width = 150,
            Height = 40
        };
        btnAdmin.Click += (s, e) => RegisterGeneralEntry(UserType.Admin);

        Button btnVisitante = new Button
        {
            Text = "Visitante",
            Width = 150,
            Height = 40
        };
        btnVisitante.Click += (s, e) => RegisterGeneralEntry(UserType.Visitor);

        buttonPanel.Controls.Add(btnDocente);
        buttonPanel.Controls.Add(btnAdmin);
        buttonPanel.Controls.Add(btnVisitante);

        externalTablePanel.Controls.Add(lblOtros, 0, 0);
        externalTablePanel.Controls.Add(buttonPanel, 0, 1);

        this.Controls.Add(externalTablePanel);
    }

    private async Task UpdateEntryCounter()
    {
        var result = await _mediator.Send(new GetDailyGenderCountUseCase.GetDailyGenderCountQuery(DateTime.UtcNow.Date));

        if (result.IsSuccess)
        {
            int maleCount = result.Value.MaleCount;
            int femaleCount = result.Value.FemaleCount;

            lblMaleCounter.Text = $"Ingresos de hombres: {maleCount}";
            lblFemaleCounter.Text = $"Ingresos de mujeres: {femaleCount}";
        }
        else
        {
            MessageBox.Show($"Error al obtener el conteo de ingresos: {string.Join(", ", result.Errors)}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }


    private async void RegisterGeneralEntry(UserType userType)
    {
        var result = await _mediator.Send(new RegisterEntryCommand(userType, Gender.NA, null));

        if (result.IsSuccess)
        {
            MessageBox.Show($"Registro exitoso. ID de entrada: {result.Value}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            await UpdateEntryCounter(); // 📌 Se actualiza el contador después del registro
        }
        else
        {
            MessageBox.Show($"Error al registrar la entrada: {string.Join(", ", result.Errors)}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    // 📌 Registrar entrada desde los botones de carrera y actualizar el contador
    private async void RegisterButton_Click(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.Tag is Tuple<Guid, string> tag)
        {
            Guid careerId = tag.Item1;
            string genderStr = tag.Item2;

            Gender gender = genderStr == "Male" ? Gender.Male : Gender.Female;
            UserType userType = UserType.Student;

            var result = await _mediator.Send(new RegisterEntryCommand(userType, gender, careerId));

            if (result.IsSuccess)
            {
                MessageBox.Show($"Registro exitoso. ID de entrada: {result.Value}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await UpdateEntryCounter(); // 📌 Se actualiza el contador después del registro
            }
            else
            {
                MessageBox.Show($"Error al registrar la entrada: {string.Join(", ", result.Errors)}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    private void panel1_Paint(object sender, PaintEventArgs e)
    {

    }

    private void pnlButtons_Paint(object sender, PaintEventArgs e)
    {

    }
}
