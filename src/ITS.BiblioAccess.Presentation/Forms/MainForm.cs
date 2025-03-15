using ITS.BiblioAccess.Presentation.Forms.Careers;
using ITS.BiblioAccess.Presentation.Forms.SystemConfigurations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ITS.BiblioAccess.Presentation.Forms
{
    public partial class MainForm : Form
    {
        private readonly IServiceProvider _serviceProvider;
        public MainForm(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        private void btnCareers_Click(object sender, EventArgs e)
        {
            var careersForm = _serviceProvider.GetRequiredService<CareersForm>();
            careersForm.Show();
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            var systemConfigurationForm = _serviceProvider.GetRequiredService<SystemConfigurationForm>();
            systemConfigurationForm.Show();
        }
    }
}
