using System;

namespace Itenium.Timesheet.Core
{
    public class ProjectDetails
    {
        public string ConsultantName { get; set; }
        public int Year { get; set; }

        public string Customer { get; set; }
        public string CustomerReference { get; set; }

        public string ProjectName { get; set; }
        public bool IsFreelancer { get; set; }

        public ProjectDetails()
        {
            Year = DateTime.Now.Year;
        }

        public override string ToString() => $"{ConsultantName} ({Year}) @{Customer}";
    }
}
