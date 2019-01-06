using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Itenium.Timesheet.Core;
using Newtonsoft.Json;

namespace Itenium.Timesheet.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            int year = DateTime.Now.Year;
            DirectoryInfo currentDllPath = new FileInfo(Environment.GetCommandLineArgs()[0]).Directory;

            IEnumerable<ProjectDetails> templates = ProjectDetailsFactory.CreateForYear(year);
            IEnumerable<ProjectDetails> projects = ProjectDetailsFactory.CreateForProjects(currentDllPath, year);

            System.Console.WriteLine("Timesheet(s) created:");
            foreach (var timesheet in templates.Union(projects))
            {
                byte[] excel = Timesheets.Create(timesheet);
                string fileName = timesheet.GetFilename(currentDllPath);

                File.WriteAllBytes(fileName, excel);
                System.Console.WriteLine(fileName);
            }
        }
    }
}
