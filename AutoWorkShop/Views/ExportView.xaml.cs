using System.Windows;
using System.Windows.Controls;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace AutoWorkshop.Views
{
    public partial class ExportView : UserControl
    {
        public ExportView()
        {
            InitializeComponent();
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                $"Экспорт_{DateTime.Now:yyyyMMdd_HHmmss}.txt");

            var content = "ЭКСПОРТ ДАННЫХ\n\n";
            if (ExportClients.IsChecked == true) content += "✓ Клиенты\n";
            if (ExportCars.IsChecked == true) content += "✓ Автомобили\n";
            if (ExportOrders.IsChecked == true) content += "✓ Заказы\n";
            if (ExportParts.IsChecked == true) content += "✓ Запчасти\n";
            if (ExportEmployees.IsChecked == true) content += "✓ Сотрудники\n";
            if (ExportFinance.IsChecked == true) content += "✓ Финансы\n";

            File.WriteAllText(path, content);

            MessageBox.Show($"Данные экспортированы:\n{path}", "Информация",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SelectAll_Click(object sender, RoutedEventArgs e)
        {
            ExportClients.IsChecked = true;
            ExportCars.IsChecked = true;
            ExportOrders.IsChecked = true;
            ExportParts.IsChecked = true;
            ExportEmployees.IsChecked = true;
            ExportFinance.IsChecked = true;
        }
    }
}