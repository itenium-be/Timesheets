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
            if (DateTime.Now.Month >= 12)
                year++;

            DirectoryInfo currentDllPath = new FileInfo(Environment.GetCommandLineArgs()[0]).Directory;

            IEnumerable<ProjectDetails> templates = ProjectDetailsFactory.CreateForYear(year);
            IEnumerable<ProjectDetails> projects = ProjectDetailsFactory.CreateForProjects(currentDllPath, year);

            System.Console.WriteLine("Timesheet(s) created:");
            foreach (ProjectDetails projectDetails in templates.Union(projects))
            {
                var builder = ExcelSheetBuilderBase.CreateBuilder(projectDetails);
                byte[] excel = builder.Build(projectDetails.Year);
                string fileName = projectDetails.GetFilename(currentDllPath);

                File.WriteAllBytes(fileName, excel);
                System.Console.WriteLine(fileName);
            }

            CreateKmVergoedingTemplate(year, currentDllPath);
        }

        private static void CreateKmVergoedingTemplate(int year, DirectoryInfo currentDllPath)
        {
            System.Console.WriteLine("");
            System.Console.WriteLine("");
            System.Console.WriteLine("Km vergoeding created:");
            var builder = new KmVergoedingBuilder();
            byte[] excel = builder.Build(year);
            string fileName = currentDllPath.FullName + $"\\itenium-kmvergoeding-{year}.xlsx";

            File.WriteAllBytes(fileName, excel);
            System.Console.WriteLine(fileName);
        }
    }
}
