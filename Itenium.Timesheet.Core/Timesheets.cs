using System;
using System.Globalization;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Itenium.Timesheet.Core
{
    public class Timesheets
    {
        public static byte[] Create(ProjectDetails projectDetails)
        {
            using (var package = new ExcelPackage())
            {
                AddWorkaroundStyles(package);

                for (int month = 1; month <= 12; month++)
                {
                    string monthName = CultureInfo.InvariantCulture.DateTimeFormat.GetAbbreviatedMonthName(month);
                    var sheet = package.Workbook.Worksheets.Add(monthName);

                    var builder = new ExcelSheetBuilder(sheet, projectDetails, month);
                    builder.Build();
                }

                package.Workbook.Worksheets[DateTime.Now.Month].Select();

                return package.GetAsByteArray();
            }
        }

        private static void AddWorkaroundStyles(ExcelPackage package)
        {
            var leftStyle = package.Workbook.Styles.CreateNamedStyle("Left");
            leftStyle.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

            var centerStyle = package.Workbook.Styles.CreateNamedStyle("Center");
            centerStyle.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            var rightStyle = package.Workbook.Styles.CreateNamedStyle("Right");
            rightStyle.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
        }
    }
}
