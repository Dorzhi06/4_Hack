using Hack_4.Classes;
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
    /// Логика взаимодействия для ReadeingPages.xaml
    /// </summary>
    public partial class ReadeingPages : Page
    {
        private List<ReadingTable> readings = new List<ReadingTable>();
        public ReadeingPages(List<ReadingTable> readings)
        {
            InitializeComponent();
            this.readings = readings;
            LoadData();
        }

        /// <summary>
        /// Загрузка данных в DataGrid
        /// </summary>
        private void LoadData()
        {
            ourTable.ItemsSource = readings;
        }
    }
}
