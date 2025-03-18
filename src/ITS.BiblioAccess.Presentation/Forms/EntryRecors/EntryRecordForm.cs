using ITS.BiblioAccess.Application.UseCases.EntryRecords.Queries;
using System.Windows.Forms.DataVisualization.Charting;
using ITS.BiblioAccess.Domain.Entities;
using ITS.BiblioAccess.Domain.ValueObjects;
using MediatR;
using System.Runtime.Versioning;
using static ITS.BiblioAccess.Application.UseCases.Careers.Queries.GetAllActiveCareersUseCase;
using static ITS.BiblioAccess.Application.UseCases.EntryRecords.Commands.RegisterEntryUseCase;
using static ITS.BiblioAccess.Application.UseCases.EntryRecords.Queries.GetDailyCareerCountUseCase;
using static ITS.BiblioAccess.Application.UseCases.EntryRecords.Queries.GetDailyGenderCountUseCase;

namespace ITS.BiblioAccess.Presentation.Forms.EntryRecors;

[SupportedOSPlatform("windows")]
public partial class EntryRecordForm : Form
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IMediator _mediator;

    private Label lblMaleCounter;
    private Label lblFemaleCounter;
    private Label lblTotalCounter;

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
        pnlButtons.ColumnCount = 2; // 🔹 2 columnas: 1 para la gráfica, 1 para los botones
        pnlButtons.RowCount = 1;
        pnlButtons.AutoSize = true;
        pnlButtons.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        pnlButtons.Dock = DockStyle.Top;
        pnlButtons.ColumnStyles.Clear();
        pnlButtons.RowStyles.Clear();

        pnlButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F)); // 40% para la gráfica
        pnlButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F)); // 60% para botones y contadores

        // -- Panel para la gráfica
        Chart chartPie = new Chart
        {
            Dock = DockStyle.Fill
        };
        ChartArea chartArea = new ChartArea("PieArea");
        chartPie.ChartAreas.Add(chartArea);
        Series seriesPie = new Series("CareerPie")
        {
            ChartType = SeriesChartType.Pie
        };
        seriesPie["PieLabelStyle"] = "Outside";
        seriesPie["OutsideLabelPlacement"] = "Right";
        seriesPie["PieLineColor"] = "Black";
        chartPie.Series.Add(seriesPie);

        var dailyCareerCountResult = await _mediator.Send(new GetDailyCareerCountQuery());

        if (dailyCareerCountResult.IsSuccess)
        {
            var careerCounts = dailyCareerCountResult.Value;
            seriesPie.Points.Clear();
            foreach (var item in careerCounts)
            {
                int pointIndex = seriesPie.Points.AddXY(item.CareerName, item.Count);
                seriesPie.Points[pointIndex].Label = $"{item.CareerName} ({item.Count})";
            }
        }
        else
        {
            MessageBox.Show($"Error al obtener conteos por carrera: {string.Join(", ", dailyCareerCountResult.Errors)}",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        pnlButtons.Controls.Add(chartPie, 0, 0); // 📌 Agregar la gráfica en la primera columna

        // -- Panel derecho para contadores y botones
        TableLayoutPanel rightPanel = new TableLayoutPanel
        {
            ColumnCount = 1,
            RowCount = 2,
            AutoSize = true,
            Dock = DockStyle.Fill
        };

        // -- Contadores de género
        TableLayoutPanel genderCounterPanel = new TableLayoutPanel
        {
            ColumnCount = 3,
            RowCount = 1,
            AutoSize = true,
            Anchor = AnchorStyles.None,
            Dock = DockStyle.Top,
            Padding = new Padding(0, 10, 0, 20)
        };

        genderCounterPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
        genderCounterPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
        genderCounterPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));

        lblMaleCounter = new Label
        {
            Text = "Ingresos de hombres: 0",
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font("Arial", 12, FontStyle.Bold),
            AutoSize = true
        };

        lblFemaleCounter = new Label
        {
            Text = "Ingresos de mujeres: 0",
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font("Arial", 12, FontStyle.Bold),
            AutoSize = true
        };

        lblTotalCounter = new Label
        {
            Text = "Ingresos totales: 0",
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font("Arial", 12, FontStyle.Bold),
            AutoSize = true
        };

        genderCounterPanel.Controls.Add(lblMaleCounter, 0, 0);
        genderCounterPanel.Controls.Add(lblFemaleCounter, 1, 0);
        genderCounterPanel.Controls.Add(lblTotalCounter, 2, 0);
        rightPanel.Controls.Add(genderCounterPanel, 0, 0);

        await UpdateEntryCounter();

        // -- Panel para los botones de carreras
        FlowLayoutPanel buttonPanel = new FlowLayoutPanel
        {
            FlowDirection = FlowDirection.LeftToRight,
            AutoSize = true,
            Dock = DockStyle.Top
        };

        var result = await _mediator.Send(new GetAllActiveCareersQuery());
        if (!result.IsSuccess)
        {
            MessageBox.Show("Error al obtener las carreras.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        List<Career> careers = result.Value;

        foreach (var career in careers)
        {
            TableLayoutPanel panelContainer = new TableLayoutPanel
            {
                ColumnCount = 1,
                RowCount = 2,
                AutoSize = true,
                Dock = DockStyle.Fill,
                Padding = new Padding(2),
                Margin = new Padding(5)
            };

            Label lblCareer = new Label
            {
                Text = career.Name.Value,
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = true,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };

            FlowLayoutPanel flowButtons = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true
            };

            Button btnMale = new Button
            {
                Text = "Hombre",
                Tag = Tuple.Create(career.CareerId, "Male"),
                Width = 100,
                Height = 40,
                Margin = new Padding(5)
            };
            btnMale.Click += RegisterButton_Click;

            Button btnFemale = new Button
            {
                Text = "Mujer",
                Tag = Tuple.Create(career.CareerId, "Female"),
                Width = 100,
                Height = 40,
                Margin = new Padding(5)
            };
            btnFemale.Click += RegisterButton_Click;

            flowButtons.Controls.Add(btnMale);
            flowButtons.Controls.Add(btnFemale);

            panelContainer.Controls.Add(lblCareer, 0, 0);
            panelContainer.Controls.Add(flowButtons, 0, 1);

            buttonPanel.Controls.Add(panelContainer);
        }

        rightPanel.Controls.Add(buttonPanel, 0, 1);

        pnlButtons.Controls.Add(rightPanel, 1, 0); // 📌 Agregar panel derecho en la segunda columna





        // -- Sección para "Otros" (Docente, Administrativo, Visitante)
        TableLayoutPanel externalTablePanel = new TableLayoutPanel
        {
            ColumnCount = 1,
            RowCount = 2,
            AutoSize = true,
            Dock = DockStyle.Top,
            Padding = new Padding(0, 10, 0, 0)
        };

        Label lblOtros = new Label
        {
            Text = "Otros",
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font("Arial", 12, FontStyle.Bold),
            AutoSize = true
        };

        FlowLayoutPanel otherButtonPanel = new FlowLayoutPanel
        {
            FlowDirection = FlowDirection.LeftToRight,
            AutoSize = true
        };

        Button btnDocente = new Button { Text = "Docente", Width = 150, Height = 40 };
        btnDocente.Click += (s, e) => RegisterGeneralEntry(UserType.Docent);

        Button btnAdmin = new Button { Text = "Administrativo", Width = 150, Height = 40 };
        btnAdmin.Click += (s, e) => RegisterGeneralEntry(UserType.Admin);

        Button btnVisitante = new Button { Text = "Visitante", Width = 150, Height = 40 };
        btnVisitante.Click += (s, e) => RegisterGeneralEntry(UserType.Visitor);

        otherButtonPanel.Controls.Add(btnDocente);
        otherButtonPanel.Controls.Add(btnAdmin);
        otherButtonPanel.Controls.Add(btnVisitante);

        externalTablePanel.Controls.Add(lblOtros, 0, 0);
        externalTablePanel.Controls.Add(otherButtonPanel, 0, 1);

        rightPanel.Controls.Add(externalTablePanel, 0, 2); // 📌 Agregar la sección "Otros" debajo de los botones de carrera

        pnlButtons.Controls.Add(rightPanel, 1, 0); // 📌 Agregar el panel derecho en la segunda columna
    }

    private async Task UpdateChart()
    {
        var dailyCareerCountResult = await _mediator.Send(
            new GetDailyCareerCountQuery()
        );

        if (dailyCareerCountResult.IsSuccess)
        {
            var careerCounts = dailyCareerCountResult.Value;

            // Buscar la gráfica en los controles
            foreach (Control control in pnlButtons.Controls)
            {
                if (control is Chart chart)
                {
                    Series seriesPie = chart.Series["CareerPie"];
                    seriesPie.Points.Clear();

                    foreach (var item in careerCounts)
                    {
                        int pointIndex = seriesPie.Points.AddXY(item.CareerName, item.Count);
                        seriesPie.Points[pointIndex].Label = $"{item.CareerName} ({item.Count})";
                    }

                    return; // Terminar la función después de actualizar la gráfica
                }
            }
        }
        else
        {
            MessageBox.Show($"Error al obtener conteos por carrera: {string.Join(", ", dailyCareerCountResult.Errors)}",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }


    private async Task UpdateEntryCounter()
    {
        var result = await _mediator.Send(new GetDailyGenderCountQuery());

        if (result.IsSuccess)
        {
            int maleCount = result.Value.MaleCount;
            int femaleCount = result.Value.FemaleCount;
            int totalCount = maleCount + femaleCount;

            // ✅ Forzar actualización en el hilo principal de la UI
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    lblMaleCounter.Text = $"Ingresos de hombres: {maleCount}";
                    lblFemaleCounter.Text = $"Ingresos de mujeres: {femaleCount}";
                    lblTotalCounter.Text = $"Ingresos totales: {totalCount}";
                }));
            }
            else
            {
                lblMaleCounter.Text = $"Ingresos de hombres: {maleCount}";
                lblFemaleCounter.Text = $"Ingresos de mujeres: {femaleCount}";
                lblTotalCounter.Text = $"Ingresos totales: {totalCount}";
            }
        }
        else
        {
            MessageBox.Show($"Error al obtener el conteo de ingresos: {string.Join(", ", result.Errors)}",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                await UpdateEntryCounter(); // 📌 Se actualiza el contador
                await UpdateChart(); // ✅ Se actualiza la gráfica
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
