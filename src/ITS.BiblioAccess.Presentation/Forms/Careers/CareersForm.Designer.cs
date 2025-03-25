namespace ITS.BiblioAccess.Presentation.Forms.Careers
{
    partial class CareersForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CareersForm));
            dgvCareers = new DataGridView();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvCareers).BeginInit();
            SuspendLayout();
            // 
            // dgvCareers
            // 
            dgvCareers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCareers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCareers.Location = new Point(12, 26);
            dgvCareers.MultiSelect = false;
            dgvCareers.Name = "dgvCareers";
            dgvCareers.ReadOnly = true;
            dgvCareers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCareers.Size = new Size(776, 366);
            dgvCareers.TabIndex = 0;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(12, 398);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(75, 23);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "Añadir";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(93, 398);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(75, 23);
            btnEdit.TabIndex = 2;
            btnEdit.Text = "Editar";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(713, 398);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 23);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "Eliminar";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // CareersForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnDelete);
            Controls.Add(btnEdit);
            Controls.Add(btnAdd);
            Controls.Add(dgvCareers);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "CareersForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Carreras";
            Load += CareersForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvCareers).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvCareers;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
    }
}