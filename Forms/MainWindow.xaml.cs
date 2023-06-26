using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Тестовое_задание
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ReportWindow reportWindow = null;
        private string RKKTextDefault = "Выберете файл RKK";
        private string requestTextDefault = "Выберете файл с обращениями";
        public MainWindow()
        {
            InitializeComponent();
            RKKText.Text = RKKTextDefault;
            RequestText.Text = requestTextDefault;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var rkkFilePath = RKKText.Text;
            var requestFilePath = RequestText.Text;

            if(rkkFilePath == RKKTextDefault || requestFilePath == requestTextDefault)
            {
                MessageBox.Show("Пути до файлов не выбраны!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if(rkkFilePath == requestFilePath)
            {
                MessageBox.Show("Указанные пути совпадают!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var sw = new Stopwatch();
            sw.Start();
            var factory = new ReportFactory();
            string? message;
            Report? report;
            var result = factory.CreateReport(rkkFilePath, requestFilePath, out report,out message);

            if(result == false || report == null)
            {
                MessageBox.Show(message,"Ошибка",MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (reportWindow != null) reportWindow.Close();
            reportWindow = new ReportWindow(report);
            reportWindow.Show();
            sw.Stop();

            TimeLabel.Content += sw.ElapsedMilliseconds.ToString() + " ms";

        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private void FileButton_Click(object sender, RoutedEventArgs e)
        {
            var buttonName = (sender as Button).Name;
            var openFileDialog = new OpenFileDialog();
            var result = openFileDialog.ShowDialog();

            if (result != true) return;

            TextBlock selectedTextBox = (TextBlock)this.FindName(buttonName + "Text");

            selectedTextBox.Text = openFileDialog.FileName;
        }

        private void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
