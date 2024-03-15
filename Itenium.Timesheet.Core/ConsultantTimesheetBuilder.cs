﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Itenium.Timesheet.Core
{
    public class ConsultantTimesheetBuilder : ExcelSheetBuilderBase
    {
        internal const string TimesheetComment = "Fill in hours in format '08:00' for the calculations to work";
        protected override string Title => "TIMESHEET";
        protected override string Email => "timesheet@itenium.be";
        protected override string EmailText => "Please send back duly signed document by the 2nd working day of the following month to:";
        protected override IEnumerable<string> TrackColumnTitles => new [] { "# Hours" };

        private readonly ProjectDetails _details;

        public ConsultantTimesheetBuilder(ProjectDetails details)
        {
            _details = details;
        }

        protected override void AddExtra()
        {
            // Sheet.Row(10).Height *= 1.5;
            Sheet.Cells["B10:C10"].Style.WrapText = true;
            Sheet.Cells["B10:C10"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            Sheet.Column(4).Width = 3;
            Sheet.Column(5).Width = 2;
            Sheet.Column(6).Width = 3;
            Sheet.Column(7).Width = 7;
            Sheet.Column(8).Width = 7;
            Sheet.Column(9).Width = 13;
            Sheet.Column(10).Width = 3;
            Sheet.Column(11).Width = 2;

            Sheet.Cells["G10"].HeaderLabel("Project");
            Sheet.Cells["H10"].Value = _details.ProjectName;

            Sheet.Cells["H11"].StyleName = "Left";
            Sheet.Cells["G11"].HeaderLabel("Total Time");
            Sheet.Cells["H11"].Formula = $"SUM(C{StartRow + 1}:C{_endRow})";
            Sheet.Cells["H11"].Style.Numberformat.Format = "[HH]:MM";

            Sheet.Cells["H12"].StyleName = "Left";
            Sheet.Cells["G12"].HeaderLabel("Days");
            Sheet.Cells["H12"].Formula = "ROUND(I11 * 3, 2)";

            Sheet.Cells["G14"].HeaderLabel("Manager");

            Sheet.Cells["H14"].Style.Fill.PatternType = ExcelFillStyle.Solid;

            Sheet.Cells["G15"].HeaderLabel("Signature");

            Sheet.Cells[10, 5, 20, 10].Style.Border.BorderAround(ExcelBorderStyle.Medium);
            Sheet.Cells[10, 5, 20, 10].Style.Fill.PatternType = ExcelFillStyle.Solid;
            Sheet.Cells[10, 5, 20, 10].Style.Fill.BackgroundColor.SetColor(Color.White);

            Sheet.Cells["H14:I14"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            Sheet.Cells["H14:I14"].Style.Border.BorderAround(ExcelBorderStyle.Thin);



            var rng = Sheet.Cells["E24:J24"];
            rng.Merge = true;
            rng.Value = "Signature Consultant";
            rng.StyleName = "Center";
            rng.Style.Font.Bold = true;

            Sheet.Cells[23, 5, 29, 10].Style.Border.BorderAround(ExcelBorderStyle.Medium);
            Sheet.Cells[23, 5, 29, 10].Style.Fill.PatternType = ExcelFillStyle.Solid;
            Sheet.Cells[23, 5, 29, 10].Style.Fill.BackgroundColor.SetColor(Color.White);
        }

        protected override void FormatTrackingCell(ExcelRange cell)
        {
            cell.Style.Numberformat.Format = "H:MM";
        }

        protected override void AddHeaderCore()
        {
            Sheet.Cells["B6"].HeaderLabel("Consultant");
            Sheet.Cells["C6"].Value = _details.ConsultantName;

            Sheet.Cells["B7"].HeaderLabel("Month");
            Sheet.Cells["C7"].Value = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(Month) + " " + _details.Year;

            Sheet.Cells["G6"].HeaderLabel("Customer");
            Sheet.Cells["H6"].Value = _details.Customer;

            Sheet.Cells["G7"].HeaderLabel("Reference");
            Sheet.Cells["H7"].Value = _details.CustomerReference;
        }

        protected override string HeaderComment => TimesheetComment;
    }
}
