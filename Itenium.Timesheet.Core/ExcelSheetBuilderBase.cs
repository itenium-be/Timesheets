﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Itenium.Timesheet.Core
{
    public abstract class ExcelSheetBuilderBase
    {
        protected const int StartRow = 10;
        protected int _endRow;

        protected ExcelWorksheet Sheet;
        protected int Year;
        protected int Month;

        /// <summary>
        /// Sheet title
        /// </summary>
        protected abstract string Title { get; }

        /// <summary>
        /// Bottom notice text
        /// </summary>
        protected abstract string EmailText { get; }
        /// <summary>
        /// Bottom notice email
        /// </summary>
        protected abstract string Email { get; }

        /// <summary>
        /// Header text for the 'what done' column
        /// </summary>
        protected abstract string TrackColumnTitle { get; }

        public byte[] Build(int year)
        {
            Year = year;

            using (var package = new ExcelPackage())
            {
                AddWorkaroundStyles(package);

                for (int month = 1; month <= 12; month++)
                {
                    string monthName = CultureInfo.InvariantCulture.DateTimeFormat.GetAbbreviatedMonthName(month);
                    Sheet = package.Workbook.Worksheets.Add(monthName);
                    Month = month;
                    AddMonthSheet();
                }

                package.Workbook.Worksheets[DateTime.Now.Month].Select();

                return package.GetAsByteArray();
            }
        }

        public void AddMonthSheet()
        {
            Sheet.Column(1).Width = 3;
            Sheet.Row(1).Height = 10;

            Sheet.Row(2).Height = 10;
            Sheet.Row(3).Height = 30;
            Sheet.Row(4).Height = 7;

            Sheet.Column(4).Width = 5;
            Sheet.Column(11).Width = 3;

            AddHeader();

            AddMonthTable();

            AddExtra();

            Sheet.Select("C11");
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

        private void AddHeader()
        {
            var currentDllPath = new FileInfo(Environment.GetCommandLineArgs()[0]);
            string logoPath = currentDllPath.DirectoryName + @"\itenium-logo.png";
            var picture = Sheet.Drawings.AddPicture("itenium logo", Image.FromFile(logoPath));
            picture.SetPosition(28, 50);

            Sheet.Cells["F3"].Value = Title;
            Sheet.Cells["F3"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            Sheet.Cells["F3"].Style.Font.Bold = true;
            Sheet.Cells["F3"].Style.Font.Size = 18;

            Sheet.Cells["B2:J4"].Style.Border.BorderAround(ExcelBorderStyle.Medium);
            Sheet.Cells["B2:J4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            Sheet.Cells["B2:J4"].Style.Fill.BackgroundColor.SetColor(Color.White);

            Sheet.Column(2).Width = 15;

            AddHeaderCore();

            Sheet.Row(5).Height = 10;

            Sheet.Row(6).Height = 22;
            Sheet.Row(8).Height = 10;
            Sheet.Cells["B6:J8"].Style.Border.BorderAround(ExcelBorderStyle.Medium);
            Sheet.Cells["B6:J8"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            Sheet.Cells["B6:J8"].Style.Fill.BackgroundColor.SetColor(Color.White);
        }

        protected virtual void AddHeaderCore()
        {

        }

        protected virtual void AddExtra()
        {

        }

        private void AddMonthTable()
        {
            int startRow = StartRow;

            int currentRow = startRow;
            Sheet.Row(startRow).Height = 18;

            Sheet.Cells[currentRow, 2].TableHeader("Day");
            Sheet.Cells[currentRow, 3].TableHeader(TrackColumnTitle);
            Sheet.Cells[currentRow, 3, currentRow, 5].Merge = true;
            Sheet.Cells[currentRow, 2, currentRow, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            currentRow++;

            foreach (var day in GetDaysInMonth())
            {
                Sheet.Cells[currentRow, 2].Value = day;
                Sheet.Cells[currentRow, 2].StyleName = "Center";

                Sheet.Cells[currentRow, 3, currentRow, 5].Merge = true;
                Sheet.Cells[currentRow, 3].StyleName = "Center";

                if (day.IsHoliday)
                {
                    Sheet.Cells[currentRow, 3].StyleName = "";
                    Sheet.Cells[currentRow, 2, currentRow, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells[currentRow, 2, currentRow, 5].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                    Sheet.Cells[currentRow, 3].Value = day.GetHolidayDesc();
                }

                FormatTrackingCell(Sheet.Cells[currentRow, 3]);

                currentRow++;
            }

            currentRow--;
            _endRow = currentRow;

            var table = Sheet.Cells[startRow, 2, currentRow, 5];
            table.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            table.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            table.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            table.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            table.Style.Border.BorderAround(ExcelBorderStyle.Medium);
            Sheet.Cells[startRow, 2, startRow, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

            AddFooter(currentRow);
        }

        protected abstract void FormatTrackingCell(ExcelRange cell);

        private void AddFooter(int lastRow)
        {
            Sheet.Cells[lastRow + 2, 2].StyleName = "Center";
            Sheet.Cells[lastRow + 2, 2].Value = EmailText;
            Sheet.Cells[lastRow + 2, 2].Style.WrapText = true;
            Sheet.Cells[lastRow + 2, 2, lastRow + 2, 9].Merge = true;
            Sheet.Row(lastRow + 2).Height = 28;

            Sheet.Cells[lastRow + 3, 2, lastRow + 3, 9].Merge = true;
            Sheet.Cells[lastRow + 3, 2].StyleName = "Center";

            var ourEmailCell = Sheet.Cells[lastRow + 3, 2];
            ourEmailCell.Hyperlink = new Uri("mailto:" + Email, UriKind.Absolute);
            ourEmailCell.Value = Email;
            ourEmailCell.Style.Font.Color.SetColor(Color.Blue);
            ourEmailCell.Style.Font.UnderLine = true;
        }

        private IEnumerable<DayInfo> GetDaysInMonth()
        {
            for (int day = 1; day <= DateTime.DaysInMonth(Year, Month); day++)
            {
                yield return new DayInfo(Year, Month, day);
            }
        }
    }
}