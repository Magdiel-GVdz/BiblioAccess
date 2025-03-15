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
            btnCareers = new Button();
            btnConfig = new Button();
            SuspendLayout();
            // 
            // btnCareers
            // 
            btnCareers.Location = new Point(231, 24);
            btnCareers.Name = "btnCareers";
            btnCareers.Size = new Size(97, 23);
            btnCareers.TabIndex = 0;
            btnCareers.Text = "Carreras";
            btnCareers.UseVisualStyleBackColor = true;
            btnCareers.Click += btnCareers_Click;
            // 
            // btnConfig
            // 
            btnConfig.Location = new Point(231, 53);
            btnConfig.Name = "btnConfig";
            btnConfig.Size = new Size(97, 23);
            btnConfig.TabIndex = 1;
            btnConfig.Text = "Configuracion";
            btnConfig.UseVisualStyleBackColor = true;
            btnConfig.Click += btnConfig_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnConfig);
            Controls.Add(btnCareers);
            Name = "MainForm";
            Text = "BiblioAccess";
            ResumeLayout(false);
        }

        #endregion

        private Button btnCareers;
        private Button btnConfig;
    }
}