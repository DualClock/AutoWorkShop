using System.Windows;
using System.Windows.Controls;
using AutoWorkshop.Services;
using AutoWorkshop.Models;
using System.Linq;
using System.IO;

namespace AutoWorkshop.Views
{
    public partial class FinanceView : UserControl
    {
        public FinanceView()
        {
            InitializeComponent();
            LoadFinance();
        }

        private void LoadFinance()
        {
            using (var db = new AppDbContext())
            {
                var orders = db.Orders.ToList();
                FinanceDataGrid.ItemsSource = orders;

                var total = orders.Sum(o => o.TotalCost);
                var avg = orders.Count > 0 ? total / orders.Count : 0;

                TotalRevenueText.Text = $"{total:N0} ₽";
                AverageCheckText.Text = $"{avg:N0} ₽";
                TotalOrdersText.Text = orders.Count.ToString();
            }
        }

        private void ExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "Финансы_Отчёт.csv");

            using (var db = new AppDbContext())
            {
                var orders = db.Orders.ToList();
                var content = "ID;Дата;Сумма;Статус\n";
                foreach (var o in orders)
                {
                    content += $"{o.Id};{o.CreatedDate:dd.MM.yyyy};{o.TotalCost};{o.Status}\n";
                }
                File.WriteAllText(path, content);
            }

            MessageBox.Show($"Отчёт экспортирован:\n{path}", "Информация",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadFinance();
            MessageBox.Show("Данные обновлены!", "Информация",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}