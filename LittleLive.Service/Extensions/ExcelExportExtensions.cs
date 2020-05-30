using LittleLive.Core.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LittleLive.Service.Extensions
{
    public static class ExportExcelExtensions
    {
        /// <summary>
        ///  Append header without title based on column index and column name
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="excelExportType"></param>
        /// <param name="headerRowIndex"></param>
        public static void AppendHeader(this ExcelWorksheet worksheet, Type excelExportType, int headerRowIndex)
        {
            if (!excelExportType.GetTypeInfo().IsSubclassOf(typeof(BaseExportHeader)))
            {
                throw new ArgumentException("Type object must be derived from BaseHeaderExport", "excelExportType");
            }

            FieldInfo[] fieldInfos = excelExportType.GetFields();

            // ADD HEADER COLUMNS           
            foreach (var fieldInfo in fieldInfos)
            {
                BaseExportColumnInfo colInfo = (BaseExportColumnInfo)fieldInfo.GetValue(null);
                worksheet.Cells[headerRowIndex, colInfo.ColumnIndex].Value = colInfo.ColumnName;
                worksheet.Cells[headerRowIndex, colInfo.ColumnIndex].Style.Font.Bold = true;
            }
        }

        /// <summary>
        /// Append header with specific title (first row) based on column index and column name
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="excelExportType"></param>
        /// <param name="withTitle"></param>
        /// <param name="titleRowIndex"></param>
        /// <param name="headerRowIndex"></param>
        public static void AppendHeader(this ExcelWorksheet worksheet, Type excelExportType, string withTitle, int titleRowIndex, int headerRowIndex)
        {
            if (!excelExportType.GetTypeInfo().IsSubclassOf(typeof(BaseExportHeader)))
            {
                throw new ArgumentException("Type object must be derived from BaseHeaderExport", "excelExportType");
            }

            FieldInfo[] fieldInfos = excelExportType.GetFields();

            if (fieldInfos.Length > 0)
            {
                FieldInfo firsField = fieldInfos.First();
                BaseExportColumnInfo firstColInfo = (BaseExportColumnInfo)firsField.GetValue(null);
                FieldInfo lastField = fieldInfos.Last();
                BaseExportColumnInfo lastColInfo = (BaseExportColumnInfo)lastField.GetValue(null);
                // SET TITLE ROW
                ExcelRange titleRange = worksheet.Cells[titleRowIndex, firstColInfo.ColumnIndex, titleRowIndex, lastColInfo.ColumnIndex];
                titleRange.Merge = true;
                titleRange.Value = withTitle;
                titleRange.Style.Font.Bold = true;
                titleRange.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                titleRange.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                // ADD HEADER COLUMNS           
                foreach (var fieldInfo in fieldInfos)
                {
                    BaseExportColumnInfo colInfo = (BaseExportColumnInfo)fieldInfo.GetValue(null);
                    worksheet.Cells[headerRowIndex, colInfo.ColumnIndex].Value = colInfo.ColumnName;
                    worksheet.Cells[headerRowIndex, colInfo.ColumnIndex].Style.Font.Bold = true;
                }
            }
        }

        /// <summary>
        /// Fix column with base on export types
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="excelExportType"></param>
        public static void AutoFixColumns(this ExcelWorksheet worksheet, Type excelExportType)
        {
            if (!excelExportType.GetTypeInfo().IsSubclassOf(typeof(BaseExportHeader)))
            {
                throw new ArgumentException("Type object must be derived from BaseHeaderExport", "excelExportType");
            }

            FieldInfo[] fieldInfos = excelExportType.GetFields();

            // ADD HEADER COLUMNS           
            foreach (var fieldInfo in fieldInfos)
            {
                BaseExportColumnInfo colInfo = (BaseExportColumnInfo)fieldInfo.GetValue(null);
                if (colInfo.MinWidth.HasValue)
                {
                    worksheet.Column(colInfo.ColumnIndex).AutoFit((int)colInfo.MinWidth);
                }
                else
                {
                    worksheet.Column(colInfo.ColumnIndex).AutoFit();
                }
            }
        }
    }
}
