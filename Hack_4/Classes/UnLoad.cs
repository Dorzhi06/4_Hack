using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hack_4.Classes
{
    class UnLoad
    {
        /// <summary>
        /// Формирование отчета на основе данных
        /// </summary>
        /// <param name="readingTables">Массив с данными</param>
        /// <param name="doclink">Ссылка на файл</param>
        /// <returns></returns>
        static void NewExcelDoc(List<ReadingTable> readingTables, string doclink)
        {
            var doc = new ExcelPackage(doclink);
            var sheet = doc.Workbook.Worksheets.Add("Отчет");
            sheet.Column(1).Width = 17;
            sheet.Column(2).Width = 17;
            sheet.Column(4).Width = 17;
            sheet.Column(5).Width = 17;
            sheet.Column(7).Width = 17;
            sheet.Cells["A1:B1"].Merge = true;
            sheet.Cells["A1:B1"].Value = "Станция";
            sheet.Cells["A1:B1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells["A1:B1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells["C1:F1"].Merge = true;
            sheet.Cells["C1:F1"].Value = "Ошибка";
            sheet.Cells["C1:F1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells["C1:F1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells["G1"].Value = "Критичность";
            sheet.Cells["G1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells["G1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells["A1:G1"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            sheet.Cells["A1:G1"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
            int countstart = 2, countend;
            for (int c = 0; c < readingTables.Count(); c++)
            {
                countend = BuildErrors(sheet, countstart, connectDB.dataDb.Station.Find(readingTables[c].station).name, 
                    connectDB.dataDb.CauseErrors.Find(readingTables[c].idCause).name);
                BuildStationName(sheet, countstart, countend - 1, readingTables[c].station);
                BuildStatus(sheet, countstart, countend - 1, connectDB.dataDb.Station.Find(readingTables[c].status).name);
                countstart = countend;
            }
            doc.Save();
        }

        /// <summary>
        /// Поиск неисправностей
        /// </summary>
        /// <param name="id">id неисправности</param>
        /// <returns>строковый массив со следующим порядком: дата начала, дата конца, количество</returns>
        static string[] GetErrors(int id)
        {
            string[] errors = { "DateStart", "DateEnd", "Count" };
            return errors;
        }

        /// <summary>
        /// Создание ячейки с названием станции
        /// </summary>
        /// <param name="sheet">Лист excel</param>
        /// <param name="firstrow">Номер начальной строки</param>
        /// <param name="lastrow">Номер конечной строки</param>
        /// <param name="station">Название станции</param>
        static void BuildStationName(ExcelWorksheet sheet, int firstrow, int lastrow, string station)
        {
            string stationcell = "A" + firstrow.ToString() + ":B" + lastrow.ToString();
            sheet.Cells[stationcell].Merge = true;
            sheet.Cells[stationcell].Value = station;
            sheet.Cells[stationcell].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[stationcell].Style.Border.Top.Color.SetColor(Color.Black);
            sheet.Cells[stationcell].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[stationcell].Style.Border.Bottom.Color.SetColor(Color.Black);
            sheet.Cells[stationcell].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[stationcell].Style.Border.Left.Color.SetColor(Color.Black);
            sheet.Cells[stationcell].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[stationcell].Style.Border.Right.Color.SetColor(Color.Black);
            sheet.Cells[stationcell].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[stationcell].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        }
        /// <summary>
        /// Создание ячейки с текущим статусом проблемы
        /// </summary>
        /// <param name="sheet">Лист excel</param>
        /// <param name="firstrow">Номер начальной строки</param>
        /// <param name="lastrow">Номер конечной строки</param>
        /// <param name="status">Статус</param>
        static void BuildStatus(ExcelWorksheet sheet, int firstrow, int lastrow, string status)
        {
            string statuscell = "G" + firstrow.ToString() + ":G" + lastrow.ToString();
            sheet.Cells[statuscell].Merge = true;
            sheet.Cells[statuscell].Value = status;
            sheet.Cells[statuscell].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[statuscell].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[statuscell].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[statuscell].Style.Border.Top.Color.SetColor(Color.Black);
            sheet.Cells[statuscell].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[statuscell].Style.Border.Bottom.Color.SetColor(Color.Black);
            sheet.Cells[statuscell].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[statuscell].Style.Border.Left.Color.SetColor(Color.Black);
            sheet.Cells[statuscell].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[statuscell].Style.Border.Right.Color.SetColor(Color.Black);
            sheet.Cells[statuscell].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[statuscell].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        }
        /// <summary>
        /// Создание полей с неисправностями
        /// </summary>
        /// <param name="sheet">Лист excel</param>
        /// <param name="firstrow">Номер начальной строки</param>
        /// <param name="errorid">Идентефикаторы несправностей</param>
        /// <param name="errorname">Название неисправности</param>
        /// <returns>Возвращает значение, сколько строк эти поля заняли (для расчета размера полей названия станции и статуса)</returns>
        static int BuildErrors(ExcelWorksheet sheet, int firstrow, string errorid, string errorname)
        {
            string errornamerow = "C" + firstrow.ToString() + ":F" + firstrow.ToString();
            sheet.Cells[errornamerow].Merge = true;
            sheet.Cells[errornamerow].Value = errorname;
            sheet.Cells[errornamerow].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[errornamerow].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[errornamerow].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[errornamerow].Style.Border.Top.Color.SetColor(Color.Black);
            sheet.Cells[errornamerow].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[errornamerow].Style.Border.Bottom.Color.SetColor(Color.Black);
            sheet.Cells[errornamerow].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[errornamerow].Style.Border.Left.Color.SetColor(Color.Black);
            sheet.Cells[errornamerow].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[errornamerow].Style.Border.Right.Color.SetColor(Color.Black);
            string[] errors = errorid.Split(';');
            int rowcount = firstrow + 1;
            foreach (string er in errors)
            {
                sheet.Cells["C" + rowcount].Value = Int32.Parse(er);
                sheet.Cells["D" + rowcount].Value = GetErrors(Int32.Parse(er))[0];
                sheet.Cells["E" + rowcount].Value = GetErrors(Int32.Parse(er))[1];
                sheet.Cells["F" + rowcount].Value = GetErrors(Int32.Parse(er))[2];
                rowcount++;
            }
            return rowcount;
        }
    }
}
