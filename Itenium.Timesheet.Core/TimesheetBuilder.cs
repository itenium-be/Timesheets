using System;
using System.Drawing;
using System.Globalization;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Itenium.Timesheet.Core
{
    public class TimesheetBuilder : ExcelSheetBuilderBase
    {
        protected override string Title => "TIMESHEET";
        protected override string Email => _details.IsFreelancer ? TimesheetEmailFreelancer : TimesheetEmailConsultant;
        protected override string EmailText => _details.IsFreelancer ? TimesheetNoticeFreelancer : TimesheetNoticeConsultant;
        protected override string TrackColumnTitle => "# Hours";

        private readonly ProjectDetails _details;

        private const string TimesheetEmailFreelancer = "invoice@itenium.be";
        private const string TimesheetEmailConsultant = "timesheet@itenium.be";
        private const string TimesheetNoticeFreelancer = "Please send back duly signed document together with your invoice by the 2nd working day of the following month to:";
        private const string TimesheetNoticeConsultant = "Please send back duly signed document by the 2nd working day of the following month to:";

        public TimesheetBuilder(ProjectDetails details)
        {
            _details = details;
        }

        protected override void AddExtra()
        {
            Sheet.Column(6).Width = 3;
            Sheet.Column(7).Width = 2;
            Sheet.Column(9).Width = 27;
            Sheet.Column(10).Width = 2;

            Sheet.Cells["H10"].HeaderLabel("Project");
            Sheet.Cells["I10"].Value = _details.ProjectName;

            Sheet.Cells["I11"].StyleName = "Left";
            Sheet.Cells["H11"].HeaderLabel("Total Time");
            Sheet.Cells["I11"].Formula = $"SUM(C{StartRow + 1}:C{_endRow})";
            Sheet.Cells["I11"].Style.Numberformat.Format = "[HH]:MM";

            Sheet.Cells["I12"].StyleName = "Left";
            Sheet.Cells["H12"].HeaderLabel("Days");
            Sheet.Cells["I12"].Formula = "ROUND(I11 * 3, 2)";

            Sheet.Cells["H14"].HeaderLabel("Manager");

            Sheet.Cells["I14"].Style.Fill.PatternType = ExcelFillStyle.Solid;

            Sheet.Cells["H15"].HeaderLabel("Signature");

            Sheet.Cells[10, 7, 20, 10].Style.Border.BorderAround(ExcelBorderStyle.Medium);
            Sheet.Cells[10, 7, 20, 10].Style.Fill.PatternType = ExcelFillStyle.Solid;
            Sheet.Cells[10, 7, 20, 10].Style.Fill.BackgroundColor.SetColor(Color.White);

            Sheet.Cells["I14"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            Sheet.Cells["I14"].Style.Border.BorderAround(ExcelBorderStyle.Thin);


            
            var rng = Sheet.Cells["G24:J24"];
            rng.Merge = true;
            rng.Value = "Signature " + ("Consultant");
            rng.StyleName = "Center";
            rng.Style.Font.Bold = true;

            Sheet.Cells[23, 7, 29, 10].Style.Border.BorderAround(ExcelBorderStyle.Medium);
            Sheet.Cells[23, 7, 29, 10].Style.Fill.PatternType = ExcelFillStyle.Solid;
            Sheet.Cells[23, 7, 29, 10].Style.Fill.BackgroundColor.SetColor(Color.White);
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

            Sheet.Cells["H6"].HeaderLabel("Customer");
            Sheet.Cells["I6"].Value = _details.Customer;

            Sheet.Cells["H7"].HeaderLabel("Reference");
            Sheet.Cells["I7"].Value = _details.CustomerReference;
        }
    }
}
