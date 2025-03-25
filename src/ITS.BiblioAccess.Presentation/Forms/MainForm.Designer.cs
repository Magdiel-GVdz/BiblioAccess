namespace ITS.BiblioAccess.Presentation.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            btnCareers = new Button();
            btnConfig = new Button();
            btnEntryRecords = new Button();
            btnExport = new Button();
            SuspendLayout();
            // 
            // btnCareers
            // 
            btnCareers.Location = new Point(64, 42);
            btnCareers.Name = "btnCareers";
            btnCareers.Size = new Size(117, 23);
            btnCareers.TabIndex = 0;
            btnCareers.Text = "Carreras";
            btnCareers.UseVisualStyleBackColor = true;
            btnCareers.Click += btnCareers_Click;
            // 
            // btnConfig
            // 
            btnConfig.Location = new Point(64, 71);
            btnConfig.Name = "btnConfig";
            btnConfig.Size = new Size(117, 23);
            btnConfig.TabIndex = 1;
            btnConfig.Text = "Configuracion";
            btnConfig.UseVisualStyleBackColor = true;
            btnConfig.Click += btnConfig_Click;
            // 
            // btnEntryRecords
            // 
            btnEntryRecords.Location = new Point(64, 100);
            btnEntryRecords.Name = "btnEntryRecords";
            btnEntryRecords.Size = new Size(117, 23);
            btnEntryRecords.TabIndex = 2;
            btnEntryRecords.Text = "Registrar Ingresos";
            btnEntryRecords.UseVisualStyleBackColor = true;
            btnEntryRecords.Click += btnEntryRecords_Click;
            // 
            // btnExport
            // 
            btnExport.Location = new Point(64, 129);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(117, 23);
            btnExport.TabIndex = 3;
            btnExport.Text = "Exportar a Excel";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += btnExport_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(245, 190);
            Controls.Add(btnExport);
            Controls.Add(btnEntryRecords);
            Controls.Add(btnConfig);
            Controls.Add(btnCareers);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "BiblioAccess";
            Load += MainForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btnCareers;
        private Button btnConfig;
        private Button btnEntryRecords;
        private Button btnExport;
    }
}