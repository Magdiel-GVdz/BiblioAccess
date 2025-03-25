namespace ITS.BiblioAccess.Presentation.Forms.Reports
{
    partial class ExcelReportsForm
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
            btnExportByDateRange = new Button();
            dtpExportByDay = new DateTimePicker();
            label2 = new Label();
            label3 = new Label();
            dtpExportStartDate = new DateTimePicker();
            dtpExportEndDay = new DateTimePicker();
            label4 = new Label();
            label5 = new Label();
            btnExportByDay = new Button();
            SuspendLayout();
            // 
            // btnExportByDateRange
            // 
            btnExportByDateRange.Location = new Point(147, 286);
            btnExportByDateRange.Name = "btnExportByDateRange";
            btnExportByDateRange.Size = new Size(75, 23);
            btnExportByDateRange.TabIndex = 1;
            btnExportByDateRange.Text = "Exportar";
            btnExportByDateRange.UseVisualStyleBackColor = true;
            btnExportByDateRange.Click += btnExportByDateRange_Click;
            // 
            // dtpExportByDay
            // 
            dtpExportByDay.Location = new Point(22, 56);
            dtpExportByDay.Name = "dtpExportByDay";
            dtpExportByDay.Size = new Size(200, 23);
            dtpExportByDay.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(22, 22);
            label2.Name = "label2";
            label2.Size = new Size(163, 15);
            label2.TabIndex = 3;
            label2.Text = "Exportar por un día especifico";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(22, 167);
            label3.Name = "label3";
            label3.Size = new Size(158, 15);
            label3.TabIndex = 4;
            label3.Text = "Exportar por rango de fechas";
            label3.Click += label3_Click;
            // 
            // dtpExportStartDate
            // 
            dtpExportStartDate.Location = new Point(22, 213);
            dtpExportStartDate.Name = "dtpExportStartDate";
            dtpExportStartDate.Size = new Size(200, 23);
            dtpExportStartDate.TabIndex = 5;
            // 
            // dtpExportEndDay
            // 
            dtpExportEndDay.Location = new Point(22, 257);
            dtpExportEndDay.Name = "dtpExportEndDay";
            dtpExportEndDay.Size = new Size(200, 23);
            dtpExportEndDay.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(22, 239);
            label4.Name = "label4";
            label4.Size = new Size(71, 15);
            label4.TabIndex = 7;
            label4.Text = "Fecha de fin";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(22, 195);
            label5.Name = "label5";
            label5.Size = new Size(86, 15);
            label5.TabIndex = 8;
            label5.Text = "Fecha de inicio";
            // 
            // btnExportByDay
            // 
            btnExportByDay.Location = new Point(147, 85);
            btnExportByDay.Name = "btnExportByDay";
            btnExportByDay.Size = new Size(75, 23);
            btnExportByDay.TabIndex = 9;
            btnExportByDay.Text = "Exportar";
            btnExportByDay.UseVisualStyleBackColor = true;
            btnExportByDay.Click += btnExportByDay_Click;
            // 
            // ExcelReportsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(251, 342);
            Controls.Add(btnExportByDay);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(dtpExportEndDay);
            Controls.Add(dtpExportStartDate);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(dtpExportByDay);
            Controls.Add(btnExportByDateRange);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "ExcelReportsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Exportar a Excel";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btnExport;
        private DateTimePicker dtpExportByDay;
        private Label label2;
        private Label label3;
        private DateTimePicker dtpExportStartDate;
        private DateTimePicker dtpExportEndDay;
        private Label label4;
        private Label label5;
        private Button btnExportByDay;
        private Button btnExportByDateRange;
    }
}