using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Itenium.Timesheet.Core
{
    public class KmVergoedingBuilder : ExcelSheetBuilderBase
    {
        private const decimal CentPerKm = 15;
        private const decimal MonthMaxEuros = 100;

        protected override string Title => "KM VERGOEDING";
        protected override string Email => "expenses@itenium.be";
        protected override string EmailText => "Please send back duly signed the following month to:";
        protected override IEnumerable<string> TrackColumnTitles => new[] { "# Kms 🚴" };

        protected override void AddExtra()
        {
            Sheet.Column(4).Width = 3;
            Sheet.Column(5).Width = 2;
            Sheet.Column(6).Width = 7;
            Sheet.Column(7).Width = 7;
            Sheet.Column(8).Width = 7;
            Sheet.Column(9).Width = 7;
            Sheet.Column(10).Width = 5;
            Sheet.Column(11).Width = 2;

            Sheet.Cells["G11"].HeaderLabel("Total 🚲");
            Sheet.Cells["H11"].StyleName = "Left";
            Sheet.Cells["H11"].Formula = $"SUM(C{StartRow + 1}:C{_endRow})";

            Sheet.Cells["F13"].HeaderLabel("Signature Consultant", "Left");

            Sheet.Cells[10, 5, 18, 10].Style.Border.BorderAround(ExcelBorderStyle.Medium);
            Sheet.Cells[10, 5, 18, 10].Style.Fill.PatternType = ExcelFillStyle.Solid;
            Sheet.Cells[10, 5, 18, 10].Style.Fill.BackgroundColor.SetColor(Color.White);
        }

        protected override void AddHeaderCore()
        {
            Sheet.Cells["B6"].HeaderLabel("Consultant");
            //Sheet.Cells["C6"].Value = _details.ConsultantName;

            Sheet.Cells["B7"].HeaderLabel("Month");
            Sheet.Cells["C7"].Value = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(Month) + " " + Year;
        }

        protected override void BuildCore(ExcelPackage package)
        {
            var readmeSheet = Sheet.Workbook.Worksheets.Add("Readme");
            readmeSheet.Cells["A1"].Value = $"{CentPerKm}cent / km. Max per maand {MonthMaxEuros} EUR";
        }

        protected override void FormatTrackingCell(ExcelRange cell)
        {
            cell.Style.Numberformat.Format = "0.0";
        }
    }
}