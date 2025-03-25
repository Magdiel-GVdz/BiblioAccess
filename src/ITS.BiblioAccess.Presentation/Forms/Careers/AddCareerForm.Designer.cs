namespace ITS.BiblioAccess.Presentation.Forms.Careers
{
    partial class AddCareerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddCareerForm));
            btnAdd = new Button();
            lblName = new Label();
            txtName = new TextBox();
            SuspendLayout();
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(158, 92);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(75, 23);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "Añadir";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(30, 33);
            lblName.Name = "lblName";
            lblName.Size = new Size(54, 15);
            lblName.TabIndex = 1;
            lblName.Text = "Nombre:";
            // 
            // txtName
            // 
            txtName.Location = new Point(30, 51);
            txtName.Name = "txtName";
            txtName.Size = new Size(203, 23);
            txtName.TabIndex = 2;
            // 
            // AddCareerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(270, 154);
            Controls.Add(txtName);
            Controls.Add(lblName);
            Controls.Add(btnAdd);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "AddCareerForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Añadir una nueva carrera";
            Load += AddCareerForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnAdd;
        private Label lblName;
        private TextBox txtName;
    }
}