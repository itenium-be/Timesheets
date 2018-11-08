﻿using OfficeOpenXml;

namespace Itenium.Timesheet.Core
{
    internal static class ExcelExtensions
    {
        public static void HeaderLabel(this ExcelRange cell, string label)
        {
            cell.Value = label + ": ";
            cell.StyleName = "Right";
            cell.Style.Font.Bold = true;
        }

        public static void TableHeader(this ExcelRange cell, string label)
        {
            cell.Value = label;
            cell.StyleName = "Center";
            cell.Style.Font.Bold = true;
        }
    }


    internal static class OtherExtensions
    {
        public static string ToOrdinal(this int num)
        {
            if (num <= 0) return num.ToString();

            switch (num % 100)
            {
                case 11:
                case 12:
                case 13:
                    return num + "th";
            }

            switch (num % 10)
            {
                case 1:
                    return num + "st";
                case 2:
                    return num + "nd";
                case 3:
                    return num + "rd";
                default:
                    return num + "th";
            }

        }
    }
}
