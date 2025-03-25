namespace ITS.BiblioAccess.Presentation.Forms.EntryRecors
{
    partial class EntryRecordForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EntryRecordForm));
            pnlButtons = new TableLayoutPanel();
            SuspendLayout();
            // 
            // pnlButtons
            // 
            pnlButtons.ColumnCount = 2;
            pnlButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            pnlButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            pnlButtons.Location = new Point(251, 212);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.RowCount = 2;
            pnlButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            pnlButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            pnlButtons.Size = new Size(200, 100);
            pnlButtons.TabIndex = 0;
            // 
            // EntryRecordForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pnlButtons);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "EntryRecordForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Registro de ingresos";
            WindowState = FormWindowState.Maximized;
            Load += EntryRecordForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel pnlButtons;
    }
}