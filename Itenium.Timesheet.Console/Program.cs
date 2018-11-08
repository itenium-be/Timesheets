using System;
using System.IO;
using Itenium.Timesheet.Core;

namespace Itenium.Timesheet.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var details = new ProjectDetails()
            {
                ConsultantName = "Jonas Vandermosten",
                IsFreelancer = false,
                Customer = "Solvay",
                CustomerReference = "",
                ProjectName = ""
            };
            var excel = Timesheets.Create(details);

            var currentDllPath = new FileInfo(Environment.GetCommandLineArgs()[0]);
            var saveExcelAs = currentDllPath.DirectoryName + $"\\itenium-timesheet-{DateTime.Now.Year}.xlsx";
            File.WriteAllBytes(saveExcelAs, excel);

            System.Console.WriteLine("Timesheet template saved as:");
            System.Console.WriteLine(saveExcelAs);
        }
    }
}
