using System.Windows;
using System.Windows.Controls;
using AutoWorkshop.Services;
using AutoWorkshop.Models;
using System.Linq;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore;

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
                var fromDate = FromDatePicker.SelectedDate ?? DateTime.Now.AddMonths(-1);
                var toDate = ToDatePicker.SelectedDate ?? DateTime.Now;
                toDate = toDate.AddDays(1);

                var stats = db.Orders
                    .Where(o => o.CreatedDate >= fromDate && o.CreatedDate <= toDate)
                    .Join(db.Cars, o => o.CarId, c => c.Id, (o, c) => new { Order = o, Car = c })
                    .GroupBy(x => x.Car.Brand)
                    .Select(g => new
                    {
                        Brand = g.Key ?? "Не указана",
                        Count = g.Count(),
                        Revenue = g.Sum(x => x.Order.TotalCost)
                    })
                    .OrderByDescending(s => s.Revenue)
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

            using (var db = new AppDbContext())
            {
                var fromDate = FromDatePicker.SelectedDate ?? DateTime.Now.AddMonths(-1);
                var toDate = ToDatePicker.SelectedDate ?? DateTime.Now;

                var stats = db.Orders
                    .Where(o => o.CreatedDate >= fromDate && o.CreatedDate <= toDate)
                    .Join(db.Cars, o => o.CarId, c => c.Id, (o, c) => new { Order = o, Car = c })
                    .GroupBy(x => x.Car.Brand)
                    .Select(g => new
                    {
                        Brand = g.Key ?? "Не указана",
                        Count = g.Count(),
                        Revenue = g.Sum(x => x.Order.TotalCost)
                    })
                    .OrderByDescending(s => s.Revenue)
                    .ToList();

                var report = new StringBuilder();
                report.AppendLine("СТАТИСТИКА ПО МАРКАМ АВТОМОБИЛЕЙ");
                report.AppendLine("=================================");
                report.AppendLine($"Период: {fromDate:dd.MM.yyyy} по {toDate:dd.MM.yyyy}");
                report.AppendLine();
                report.AppendLine($"Всего марок: {stats.Count}");
                report.AppendLine($"Общая выручка: {stats.Sum(s => s.Revenue):F2} руб.");
                report.AppendLine();
                report.AppendLine("ДЕТАЛИЗАЦИЯ:");
                report.AppendLine("------------");

                foreach (var stat in stats)
                {
                    report.AppendLine($"{stat.Brand}: {stat.Count} ремонтов, выручка {stat.Revenue:F2} руб.");
                }

                File.WriteAllText(path, report.ToString(), Encoding.UTF8);
            }

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