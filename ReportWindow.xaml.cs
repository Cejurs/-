using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Тестовое_задание.Export;

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
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            var count = 0;
            ReportDataGrid.ItemsSource = report.ReportData.Select(x => new
            {
                RowId = count++,
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
        }
    }
}
