namespace ITS.BiblioAccess.Presentation.Forms.Careers
{
    partial class ConfirmDeleteCareerform
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
            btnAccept = new Button();
            btnCancel = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // btnAccept
            // 
            btnAccept.Location = new Point(35, 97);
            btnAccept.Name = "btnAccept";
            btnAccept.Size = new Size(75, 23);
            btnAccept.TabIndex = 0;
            btnAccept.Text = "Aceptar";
            btnAccept.UseVisualStyleBackColor = true;
            btnAccept.Click += btnAccept_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(158, 97);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Cancelar";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(9, 40);
            label1.Name = "label1";
            label1.Size = new Size(255, 15);
            label1.TabIndex = 2;
            label1.Text = "¿Estas seguro que quieres eliminar esta carrera?";
            // 
            // ConfirmDeleteCareerform
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(270, 157);
            Controls.Add(label1);
            Controls.Add(btnCancel);
            Controls.Add(btnAccept);
            Name = "ConfirmDeleteCareerform";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ConfirmDeleteCareerform";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnAccept;
        private Button btnCancel;
        private Label label1;
    }
}