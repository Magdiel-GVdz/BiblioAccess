namespace ITS.BiblioAccess.Presentation.Forms.Careers
{
    partial class EditCareerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditCareerForm));
            txtName = new TextBox();
            lblName = new Label();
            btnSave = new Button();
            chkActive = new CheckBox();
            SuspendLayout();
            // 
            // txtName
            // 
            txtName.Location = new Point(30, 57);
            txtName.Name = "txtName";
            txtName.Size = new Size(203, 23);
            txtName.TabIndex = 5;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(30, 39);
            lblName.Name = "lblName";
            lblName.Size = new Size(54, 15);
            lblName.TabIndex = 4;
            lblName.Text = "Nombre:";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(158, 98);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 3;
            btnSave.Text = "Guardar";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // chkActive
            // 
            chkActive.AutoSize = true;
            chkActive.Location = new Point(30, 101);
            chkActive.Name = "chkActive";
            chkActive.Size = new Size(60, 19);
            chkActive.TabIndex = 6;
            chkActive.Text = "Activo";
            chkActive.UseVisualStyleBackColor = true;
            // 
            // EditCareerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(263, 161);
            Controls.Add(chkActive);
            Controls.Add(txtName);
            Controls.Add(lblName);
            Controls.Add(btnSave);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "EditCareerForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Editar carrera";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtName;
        private Label lblName;
        private Button btnSave;
        private CheckBox chkActive;
    }
}