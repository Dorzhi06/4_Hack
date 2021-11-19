using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hack_4.Classes
{
    class LoadDate
    {
        //Массив данных
        private static List<TimerClass> allData = new List<TimerClass>();

        /// <summary>
        /// Функция конвертации excel файлов в массив данных List
        /// </summary>
        /// <param name="doclink">Путь к excel файлу</param>
        /// <returns></returns>
        public static List<TimerClass> GetExcelData(string doclink)
        {
            var doc = new ExcelPackage(doclink).Workbook.Worksheets[0];
            int cellnum = 7;
            int start = 1;
            int id;
            string? Date, Who, Station, Object,
                TimeStart, DateEnd, TimeEnd, Ind,
                Cound, Service, Why, WhoDoing,
                WhoDateEnd, WhoTimeEnd, temp;
            do
            {
                if (Convert(doc.Cells["A" + cellnum].Value) == null) break;
                id = start;
                Date = Convert(doc.Cells["B" + cellnum].Value);
                Who = Convert(doc.Cells["C" + cellnum].Value);
                Station = Convert(doc.Cells["D" + cellnum].Value);
                Object = Convert(doc.Cells["E" + cellnum].Value);
                TimeStart = Convert(doc.Cells["F" + cellnum].Value);
                DateEnd = Convert(doc.Cells["H" + cellnum].Value).Split(' ')[0];
                TimeEnd = Convert(doc.Cells["H" + cellnum].Value).Split(' ')[1];
                Ind = Convert(doc.Cells["I" + cellnum].Value);
                Cound = Convert(doc.Cells["J" + cellnum].Value);
                Service = Convert(doc.Cells["K" + cellnum].Value);
                Why = Convert(doc.Cells["L" + cellnum].Value);

                if (Convert(doc.Cells["M" + cellnum].Value) != null)
                {
                    WhoDateEnd = null;
                    WhoTimeEnd = null;

                    Regex regexdate = new Regex(@"\d{2}.\d{2}.\d{4}");
                    Regex regextime = new Regex(@"\d{2}:\d{2}:\d{2}");

                    Match matchdate = regexdate.Match(Convert(doc.Cells["M" + cellnum].Value));
                    Match matchtime = regextime.Match(Convert(doc.Cells["M" + cellnum].Value));


                    if (matchdate.Success && matchtime.Success)
                    {
                        WhoDateEnd = matchdate.Groups[0].Value;
                        WhoTimeEnd = matchtime.Groups[0].Value;
                        temp = Convert(doc.Cells["M" + cellnum].Value);
                        temp = temp.Replace(WhoDateEnd, " ");
                        temp = temp.Replace(WhoTimeEnd, " ");
                        WhoDoing = temp.Trim();
                    }
                    else if (!matchdate.Success)
                    {
                        WhoDoing = Convert(doc.Cells["M" + cellnum].Value).Trim();
                    }
                    else
                    {
                        WhoDateEnd = null;
                        WhoDoing = null;
                        WhoTimeEnd = null;
                    }
                }
                else
                {
                    WhoDateEnd = null;
                    WhoDoing = null;
                    WhoTimeEnd = null;
                }

                TimerClass tc = new TimerClass();
                tc.id = id;
                tc.Date = Date;
                tc.Who = Who;
                tc.Station = Station;
                tc.Object = Object;
                tc.TimeStart = TimeStart;
                tc.DateEnd = DateEnd;
                tc.TimeEnd = TimeEnd;
                tc.Ind = Ind;
                tc.Cound = Int32.Parse(Cound);
                tc.Service = Service;
                tc.Why = Why;
                tc.WhoDoing = WhoDoing;
                tc.WhoDateEnd = WhoDateEnd;
                tc.WhoTimeEnd = WhoTimeEnd;
                allData.Add(tc);
                cellnum++;
                start++;
                if (start == 1000) break;

            } while (Date != null);
            return allData;
        }

        static string Convert(object obj)
        {
            string? solve;
            if (obj != null)
            {
                solve = obj.ToString();
            }
            else
            {
                solve = null;
            }
            return solve;
        }
    }
}
