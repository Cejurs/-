using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Тестовое_задание
{
    /// <summary>
    /// Логика взаимодействия для ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        private Report report;
        public ReportWindow(Report report)
        {
            InitializeComponent();
            this.report = report;
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            ReportDataGrid.ItemsSource = this.report.ReportData;
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
