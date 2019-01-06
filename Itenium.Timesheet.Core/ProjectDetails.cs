using System;
using System.IO;
using Newtonsoft.Json;

namespace Itenium.Timesheet.Core
{
    public class ProjectDetails
    {
        public string ConsultantName { get; set; }
        [JsonIgnore]
        public int Year { get; set; }

        public string Customer { get; set; }
        public string CustomerReference { get; set; }

        public string ProjectName { get; set; }
        public bool IsFreelancer { get; set; }

        [JsonIgnore]
        public string FileNameSuffix { get; set; }

        public ProjectDetails()
        {

        }

        public ProjectDetails(int? year = null, string fileNameSuffix = null)
        {
            Year = year ?? DateTime.Now.Year;
            FileNameSuffix = fileNameSuffix;
        }

        public string GetFilename(DirectoryInfo saveIn)
        {
            string name = saveIn.FullName + $"\\itenium-timesheet-{Year}";
            string fileNameSuffix = GetFileNameSuffix();
            if (!string.IsNullOrWhiteSpace(fileNameSuffix))
            {
                name += "-" + fileNameSuffix;
            }
            return name + ".xlsx";
        }

        private string GetFileNameSuffix()
        {
            if (!string.IsNullOrWhiteSpace(FileNameSuffix))
            {
                return FileNameSuffix;
            }

            if (!string.IsNullOrWhiteSpace(ConsultantName))
            {
                return ConsultantName
                    .Trim()
                    .ToLowerInvariant()
                    .Replace(" ", "-");
            }

            return null;
        }

        public override string ToString() => $"{ConsultantName} ({Year}) @{Customer}";
    }
}
