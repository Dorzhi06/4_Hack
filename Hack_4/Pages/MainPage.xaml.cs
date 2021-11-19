using Hack_4.Classes;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hack_4.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        //Путь к примерному файлу
        private string file = @"E:\Хакатон\Hack_4\Hack_4\Files\1.xlsx";

        //Массив данных
        List<MainData> allData = null;
        public MainPage()
        {
            InitializeComponent();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            LoadData();
        }

        /// <summary>
        /// Загрузка данных в DataGrid
        /// </summary>
        private void LoadData()
        {
            allData = LoadDate.GetExcelData(file);
            ourTable.ItemsSource = allData;
        }

        private void nextBtn_Click(object sender, RoutedEventArgs e)
        {
            DataProcessing dp = new DataProcessing();
            MainWindow mw = (MainWindow)Window.GetWindow(this);

            List<ReadingTable> rtList = new List<ReadingTable>();
            rtList = dp.ProccesDatas(allData, connectDB.dataDb.Station.Find(3), connectDB.dataDb.CauseErrors.Find(2));

            mw.NVG.Navigate(new Pages.ReadeingPages(rtList));
        }
    }
}
