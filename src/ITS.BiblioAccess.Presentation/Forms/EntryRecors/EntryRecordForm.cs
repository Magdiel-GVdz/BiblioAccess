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

        // -- Configuramos 4 columnas y 1 fila inicial
        pnlButtons.ColumnCount = 4;
        pnlButtons.RowCount = 1;
        pnlButtons.AutoSize = true;
        pnlButtons.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        pnlButtons.Dock = DockStyle.Top;
        pnlButtons.ColumnStyles.Clear();
        pnlButtons.RowStyles.Clear();

        // -- Definimos las cuatro columnas (la primera queda “vacía”)
        pnlButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F)); // Columna vacía
        pnlButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        pnlButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        pnlButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));

        // -- Agregamos el título en la primera fila, ocupando las 4 columnas
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
        pnlButtons.SetColumnSpan(lblTitle, 4);

        // -- Contadores por género
        TableLayoutPanel genderCounterPanel = new TableLayoutPanel
        {
            ColumnCount = 2,
            RowCount = 1,
            AutoSize = true,
            Anchor = AnchorStyles.None,
            Dock = DockStyle.None,
            Padding = new Padding(0, 10, 0, 20),
            Margin = new Padding(0, 0, 0, 10)
        };
        genderCounterPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        genderCounterPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

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

        lblTotalCounter = new Label
        {
            Text = "Ingresos totales: 0",
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font("Arial", 12, FontStyle.Bold),
            AutoSize = true,
            Anchor = AnchorStyles.None
        };



        genderCounterPanel.Controls.Add(lblMaleCounter, 0, 0);
        genderCounterPanel.Controls.Add(lblFemaleCounter, 1, 0);

        genderCounterPanel.RowCount = 2;
        genderCounterPanel.Controls.Add(lblTotalCounter, 0, 1);
        genderCounterPanel.SetColumnSpan(lblTotalCounter, 2);


        // -- Lo añadimos en la segunda fila, abarcando 4 columnas
        pnlButtons.RowCount++;
        pnlButtons.Controls.Add(genderCounterPanel, 0, 1);
        pnlButtons.SetColumnSpan(genderCounterPanel, 4);

        // -- Actualizamos el contador
        await UpdateEntryCounter();


        // 1) Agregamos una nueva fila para la gráfica
        pnlButtons.RowCount++;
        pnlButtons.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        int chartRowIndex = pnlButtons.RowCount - 1;

        // 2) Creamos el control Chart
        Chart chartPie = new Chart
        {
            Dock = DockStyle.Fill
        };

        // Configuramos el área de la gráfica
        ChartArea chartArea = new ChartArea("PieArea");
        chartPie.ChartAreas.Add(chartArea);

        // Creamos la serie como Pie
        Series seriesPie = new Series("CareerPie")
        {
            ChartType = SeriesChartType.Pie
        };
        // (Opcional) Mostrar las etiquetas en el interior
        seriesPie["PieLabelStyle"] = "Outside"; // Mueve las etiquetas fuera del gráfico
        seriesPie["OutsideLabelPlacement"] = "Right"; // Coloca las etiquetas a la derecha
        seriesPie["PieLineColor"] = "Black"; // Dibuja una línea para conectar la etiqueta con la sección


        // 3) Obtenemos los datos para la gráfica (ejemplo)
        var dailyCareerCountResult = await _mediator.Send(
            new GetDailyCareerCountQuery()
        );

        if (dailyCareerCountResult.IsSuccess)
        {
            var careerCounts = dailyCareerCountResult.Value;

            // Limpiar la serie anterior antes de agregar datos nuevos
            seriesPie.Points.Clear();

            foreach (var item in careerCounts)
            {
                int pointIndex = seriesPie.Points.AddXY(item.CareerName, item.Count);
                seriesPie.Points[pointIndex].Label = $"{item.CareerName} ({item.Count})"; // ✅ Solución

            }
        }
        else
        {
            MessageBox.Show($"Error al obtener conteos por carrera: {string.Join(", ", dailyCareerCountResult.Errors)}",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }



        // Agregamos la serie al chart
        chartPie.Series.Add(seriesPie);

        // 4) Insertamos el Chart en columna 0, en la fila que acabamos de crear
        pnlButtons.Controls.Add(chartPie, 0, chartRowIndex);


        // -- Obtenemos las carreras
        var result = await _mediator.Send(new GetAllActiveCareersQuery());
        if (!result.IsSuccess)
        {
            MessageBox.Show("Error al obtener las carreras.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        List<Career> careers = result.Value;

        // -- Aquí comenzamos en la fila 2 (ya usamos 0 para título y 1 para contadores)
        int rowIndex = 2;

        Size _buttonSize = new Size(90, 35);
        for (int i = 0; i < careers.Count; i += 3)
        {
            pnlButtons.RowCount++;
            pnlButtons.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            for (int j = 1; j <= 3; j++)
            {
                if (i + (j - 1) >= careers.Count) break;

                Career career = careers[i + (j - 1)];

                // -- Crea un panel de 2 filas (label y botones).
                var panelContainer = new TableLayoutPanel
                {
                    ColumnCount = 1,
                    RowCount = 2,
                    AutoSize = true,
                    Dock = DockStyle.Fill,

                    // Ajusta estos para reducir espacio:
                    Padding = new Padding(2),
                    Margin = new Padding(5),

                    // Fila 0 -> Label, fila 1 -> FlowLayoutPanel
                };
                panelContainer.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                panelContainer.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                // -- Label con el nombre de la carrera
                Label lblCareer = new Label
                {
                    Text = career.Name.Value,
                    TextAlign = ContentAlignment.MiddleCenter,
                    AutoSize = true,
                    Font = new Font("Arial", 10, FontStyle.Bold),
                    Margin = new Padding(0, 0, 0, 2) // margen inferior pequeño
                };

                // -- FlowLayoutPanel para los 2 botones en una fila
                FlowLayoutPanel flowButtons = new FlowLayoutPanel
                {
                    FlowDirection = FlowDirection.LeftToRight,
                    AutoSize = true,
                    Margin = new Padding(0)
                };

                // -- Botón 'Hombre'
                Button btnMale = new Button
                {
                    Text = "Hombre",
                    Tag = Tuple.Create(career.CareerId, "Male"),
                    Width = 150,
                    Height = 40,
                    Margin = new Padding(0, 0, 5, 0) // margen derecho pequeño 
                };
                btnMale.Click += RegisterButton_Click;

                // -- Botón 'Mujer'
                Button btnFemale = new Button
                {
                    Text = "Mujer",
                    Tag = Tuple.Create(career.CareerId, "Female"),
                    Width = 150,
                    Height = 40,
                    Margin = new Padding(0)
                };
                btnFemale.Click += RegisterButton_Click;

                // -- Agrega los dos botones al flowLayout
                flowButtons.Controls.Add(btnMale);
                flowButtons.Controls.Add(btnFemale);

                // -- Añade Label (fila 0) y Flow (fila 1) al panel contenedor
                panelContainer.Controls.Add(lblCareer, 0, 0);
                panelContainer.Controls.Add(flowButtons, 0, 1);

                // -- Lo ubicamos en la tabla principal, columna j (1..3)
                pnlButtons.Controls.Add(panelContainer, j, pnlButtons.RowCount - 1);
            }
        }



        // -- Sección para “Otros”
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

        // -- Agregamos este panel “externo” directamente al formulario (o podrías también meterlo en pnlButtons)
        this.Controls.Add(externalTablePanel);
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
