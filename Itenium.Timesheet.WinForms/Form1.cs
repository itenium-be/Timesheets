using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Itenium.Timesheet.Core;

namespace Itenium.Timesheet.WinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var details = new ProjectDetails()
            {
                ConsultantName = ConsultantName.Text,
                IsFreelancer = IsFreelancer.Checked,
                Customer = Customer.Text,
                CustomerReference = CustomerReference.Text,
                ProjectName = ProjectName.Text
            };
            var excel = Timesheets.Create(details);

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string saveExcelAs = desktopPath + $"\\itenium-timesheet-{DateTime.Now.Year}.xlsx";
            File.WriteAllBytes(saveExcelAs, excel);

            System.Diagnostics.Process.Start(saveExcelAs);
        }
    }
}
