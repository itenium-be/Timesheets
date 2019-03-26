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
            var details = new ProjectDetails(int.Parse(Year.Text))
            {
                ConsultantName = ConsultantName.Text,
                IsFreelancer = IsFreelancer.Checked,
                Customer = Customer.Text,
                CustomerReference = CustomerReference.Text,
                ProjectName = ProjectName.Text,
            };

            var desktopPath = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            var excel = new TimesheetBuilder(details).Build(details.Year);
            File.WriteAllBytes(details.GetFilename(desktopPath), excel);
            System.Diagnostics.Process.Start(details.GetFilename(desktopPath));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Year.Text = DateTime.Now.Year.ToString();
        }
    }
}
