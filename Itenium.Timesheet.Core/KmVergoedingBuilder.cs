using System;
using System.Drawing;
using System.Globalization;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Itenium.Timesheet.Core
{
    public class KmVergoedingBuilder : ExcelSheetBuilderBase
    {
        protected override string Title => "KM VERGOEDING";
        protected override string Email => "expenses@itenium.be";
        protected override string EmailText => "Please send back duly signed the following month to:";
        protected override string TrackColumnTitle => "# Kms 🚴";

        protected override void AddExtra()
        {
            Sheet.Column(6).Width = 3;
            Sheet.Column(7).Width = 2;
            Sheet.Column(9).Width = 27;
            Sheet.Column(10).Width = 2;

            Sheet.Cells["H11"].HeaderLabel("Total 🚲");
            Sheet.Cells["I11"].StyleName = "Left";
            Sheet.Cells["I11"].Formula = $"SUM(C{StartRow + 1}:C{_endRow})";

            Sheet.Cells["H13"].HeaderLabel("    Signature Consultant", "Left");

            Sheet.Cells[10, 7, 18, 10].Style.Border.BorderAround(ExcelBorderStyle.Medium);
            Sheet.Cells[10, 7, 18, 10].Style.Fill.PatternType = ExcelFillStyle.Solid;
            Sheet.Cells[10, 7, 18, 10].Style.Fill.BackgroundColor.SetColor(Color.White);
        }

        protected override void AddHeaderCore()
        {
            Sheet.Cells["B6"].HeaderLabel("Consultant");
            //Sheet.Cells["C6"].Value = _details.ConsultantName;

            Sheet.Cells["B7"].HeaderLabel("Month");
            Sheet.Cells["C7"].Value = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(Month) + " " + Year;
        }

        protected override void FormatTrackingCell(ExcelRange cell)
        {
            cell.Style.Numberformat.Format = "0";
        }
    }
}