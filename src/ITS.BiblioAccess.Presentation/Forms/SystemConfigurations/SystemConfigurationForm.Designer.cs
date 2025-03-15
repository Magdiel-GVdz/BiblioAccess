namespace ITS.BiblioAccess.Presentation.Forms.SystemConfigurations
{
    partial class SystemConfigurationForm
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
            dtpExportHour = new DateTimePicker();
            btnAccept = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // dtpExportHour
            // 
            dtpExportHour.Format = DateTimePickerFormat.Time;
            dtpExportHour.Location = new Point(30, 51);
            dtpExportHour.Name = "dtpExportHour";
            dtpExportHour.ShowUpDown = true;
            dtpExportHour.Size = new Size(183, 23);
            dtpExportHour.TabIndex = 0;
            // 
            // btnAccept
            // 
            btnAccept.Location = new Point(138, 91);
            btnAccept.Name = "btnAccept";
            btnAccept.Size = new Size(75, 23);
            btnAccept.TabIndex = 1;
            btnAccept.Text = "Aceptar";
            btnAccept.UseVisualStyleBackColor = true;
            btnAccept.Click += btnAccept_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(30, 33);
            label1.Name = "label1";
            label1.Size = new Size(183, 15);
            label1.TabIndex = 2;
            label1.Text = "Seleccione la hora de exportacion";
            // 
            // SystemConfigurationForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(247, 149);
            Controls.Add(label1);
            Controls.Add(btnAccept);
            Controls.Add(dtpExportHour);
            Name = "SystemConfigurationForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Configuracion";
            Load += SystemConfigurationForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DateTimePicker dtpExportHour;
        private Button btnAccept;
        private Label label1;
    }
}