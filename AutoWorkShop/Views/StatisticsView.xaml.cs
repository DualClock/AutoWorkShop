using System.Windows;
using System.Windows.Controls;
using AutoWorkshop.Services;
using AutoWorkshop.Models;
using System.Linq;
using System.IO;

namespace AutoWorkshop.Views
{
    public partial class StatisticsView : UserControl
    {
        public StatisticsView()
        {
            InitializeComponent();
            FromDatePicker.SelectedDate = DateTime.Now.AddMonths(-1);
            ToDatePicker.SelectedDate = DateTime.Now;
            LoadStatistics();
        }

        private void LoadStatistics()
        {
            using (var db = new AppDbContext())
            {
                var stats = db.Cars
                    .Join(db.Orders, c => c.Id, o => o.CarId, (c, o) => new { Car = c, Order = o })
                    .GroupBy(x => x.Car.Brand)
                    .Select(g => new
                    {
                        Brand = g.Key ?? "Не указана",
                        Count = g.Count(),
                        Revenue = g.Sum(x => x.Order.TotalCost)
                    })
                    .ToList();

                StatsDataGrid.ItemsSource = stats;
            }
        }

        private void ApplyFilter_Click(object sender, RoutedEventArgs e)
        {
            LoadStatistics();
            MessageBox.Show("Фильтр применён!", "Информация",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void GenerateReport_Click(object sender, RoutedEventArgs e)
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "Статистика_Отчёт.txt");
            File.WriteAllText(path, $"Статистика за период\n{FromDatePicker.SelectedDate} - {ToDatePicker.SelectedDate}");

            MessageBox.Show($"Отчёт сохранён:\n{path}", "Информация",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadStatistics();
            MessageBox.Show("Данные обновлены!", "Информация",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}