using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hack_4.Classes
{
    class DataProcessing
    {
        private List<MainData> datas = new List<MainData>();

        /// <summary>
        /// Функция обработки данных
        /// </summary>
        /// <param name="allData">Все данные</param>
        /// <param name="station">Станция</param>
        /// <param name="errors">Причина поломки</param>
        /// <returns>Массив обработанных данных на определенную станцию и причину инцидента</returns>
        public List<ReadingTable> ProccesDatas(List<MainData> allData, Station station, CauseErrors errors)
        {
            List<ReadingTable> readings = new List<ReadingTable>();

            ReadyTable(allData,station,errors);    

            return readings;
        }

        /// <summary>
        /// Подготовка массива данных к работе
        /// </summary>
        /// <param name="allData">Все данные</param>
        /// <param name="station">Станция</param>
        /// <param name="errors">Причина поломки</param>
        private void ReadyTable(List<MainData> allData, Station station, CauseErrors errors)
        {
            for (int i = 0; i < allData.Count(); i++)
            {
                if (allData[i].Station == station.name && allData[i].Why == errors.name)
                {
                    datas.Add(allData[i]);
                }
            }
        }


    }
}
