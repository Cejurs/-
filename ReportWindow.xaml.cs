using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Тестовое_задание.Export;
using Тестовое_задание.Sort;

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
            ExportMenu.ItemsSource = ExportService.GetExportVariants();
            SortMenu.ItemsSource = SortService.GetSortVariants();
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            var count = 0;
            ReportDataGrid.ItemsSource = report.ReportData.Select(x => new
            {
                RowId = ++count,
                x.ResponsiblePerson,
                x.UnfullfilledRequestCount,
                x.UnfullfiledDocumentCount,
                x.SummaryUnfullfiledCount
            });
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void ExportMenu_Click(object sender, RoutedEventArgs e)
        {
            var exportVariant = (e.OriginalSource as MenuItem)?.DataContext.ToString();
            if(exportVariant == null)
            {
                MessageBox.Show("Что-то пошло не так, если ты видишь это сообщение ты волшебник");
                return;
            }
            string message;
            ExportService.Export(exportVariant, report, out message);
            MessageBox.Show(message,"Экспорт",MessageBoxButton.OK,MessageBoxImage.Information);
        }

        private void SortMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var sortVariant = (e.OriginalSource as MenuItem)?.DataContext.ToString();
            if (sortVariant == null)
            {
                MessageBox.Show("Что-то пошло не так, если ты видишь это сообщение ты волшебник");
                return;
            }
            var sort = SortService.GetSort(sortVariant);
            report.Sort(sort);
            UpdateGrid();
        }
        private void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
