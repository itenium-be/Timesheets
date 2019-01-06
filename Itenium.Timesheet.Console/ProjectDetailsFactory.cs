using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Itenium.Timesheet.Core;
using Newtonsoft.Json;

namespace Itenium.Timesheet.Console
{
    internal static class ProjectDetailsFactory
    {
        /// <summary>
        /// Create timesheet for Consultants and Freelancers
        /// </summary>
        public static IEnumerable<ProjectDetails> CreateForYear(int year)
        {
            yield return ForConsultants(year);
            yield return ForFreelancers(year);
        }

        private static ProjectDetails ForFreelancers(int year)
        {
            return new ProjectDetails(year, "freelancer")
            {
                IsFreelancer = true,
            };
        }

        private static ProjectDetails ForConsultants(int year)
        {
            return new ProjectDetails(year, "consultant")
            {
                IsFreelancer = false,
            };
        }

        /// <summary>
        /// Create a template for specific Consultants/Projects
        /// </summary>
        public static IEnumerable<ProjectDetails> CreateForProjects(DirectoryInfo projectsDir, int year)
        {
            return Directory.GetFiles(Path.Combine(projectsDir.FullName, "Projects"), "*.json")
                .Select(File.ReadAllText)
                .Select(JsonConvert.DeserializeObject<ProjectDetails>)
                .Select(projectDetails =>
                {
                    projectDetails.Year = year;
                    return projectDetails;
                });
        }
    }
}