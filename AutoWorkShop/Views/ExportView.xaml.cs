using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore;
using AutoWorkshop.Services;
using AutoWorkshop.Models;
using System.Linq;

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

            var report = new StringBuilder();
            report.AppendLine("ЭКСПОРТ ДАННЫХ АВТОМАСТЕРСКОЙ");
            report.AppendLine("==============================");
            report.AppendLine($"Дата экспорта: {DateTime.Now:dd.MM.yyyy HH:mm}");
            report.AppendLine();

            using (var db = new AppDbContext())
            {
                if (ExportClients.IsChecked == true)
                {
                    var clients = db.Clients.ToList();
                    report.AppendLine("КЛИЕНТЫ");
                    report.AppendLine($"-------- ({clients.Count} записей)");
                    foreach (var c in clients)
                    {
                        report.AppendLine($"  {c.FullName} | {c.Phone} | {c.Email}");
                    }
                    report.AppendLine();
                }

                if (ExportCars.IsChecked == true)
                {
                    var cars = db.Cars.Include(c => c.Client).ToList();
                    report.AppendLine("АВТОМОБИЛИ");
                    report.AppendLine($"----------- ({cars.Count} записей)");
                    foreach (var car in cars)
                    {
                        report.AppendLine($"  {car.Brand} {car.Model} | VIN: {car.VIN} | Номер: {car.PlateNumber} | Владелец: {car.Client?.FullName}");
                    }
                    report.AppendLine();
                }

                if (ExportOrders.IsChecked == true)
                {
                    var orders = db.Orders
                        .Include(o => o.Car).ThenInclude(c => c.Client)
                        .Include(o => o.Employee)
                        .ToList();
                    report.AppendLine("ЗАКАЗЫ");
                    report.AppendLine($"------- ({orders.Count} записей)");
                    foreach (var o in orders)
                    {
                        report.AppendLine($"  Заказ #{o.Id} | {o.Car?.PlateNumber} | {o.Status} | {o.TotalCost:F2} руб. | Мастер: {o.Employee?.FullName}");
                    }
                    report.AppendLine();
                }

                if (ExportParts.IsChecked == true)
                {
                    var parts = db.Parts.ToList();
                    report.AppendLine("ЗАПЧАСТИ");
                    report.AppendLine($"--------- ({parts.Count} записей)");
                    foreach (var p in parts)
                    {
                        report.AppendLine($"  {p.Name} (арт. {p.Article}) | {p.Quantity} шт. | {p.Price:F2} руб.");
                    }
                    report.AppendLine();
                }

                if (ExportEmployees.IsChecked == true)
                {
                    var employees = db.Employees.Include(emp => emp.Department).ToList();
                    report.AppendLine("СОТРУДНИКИ");
                    report.AppendLine($"----------- ({employees.Count} записей)");
                    foreach (var emp in employees)
                    {
                        report.AppendLine($"  {emp.FullName} | {emp.Position} | {emp.Department?.Name} | {emp.Phone}");
                    }
                    report.AppendLine();
                }

                if (ExportFinance.IsChecked == true)
                {
                    var receipts = db.Receipts.Include(r => r.Order).ThenInclude(o => o.Car).ToList();
                    report.AppendLine("ФИНАНСЫ");
                    report.AppendLine($"-------- ({receipts.Count} записей)");
                    foreach (var r in receipts)
                    {
                        report.AppendLine($"  Квитанция #{r.ReceiptNumber} | {r.Order?.Car?.PlateNumber} | {r.Amount:F2} руб. | {r.PaymentDate:dd.MM.yyyy}");
                    }
                    report.AppendLine();
                }
            }

            report.AppendLine("==============================");
            report.AppendLine("Конец экспорта");

            File.WriteAllText(path, report.ToString(), Encoding.UTF8);

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

        private void UnselectAll_Click(object sender, RoutedEventArgs e)
        {
            ExportClients.IsChecked = false;
            ExportCars.IsChecked = false;
            ExportOrders.IsChecked = false;
            ExportParts.IsChecked = false;
            ExportEmployees.IsChecked = false;
            ExportFinance.IsChecked = false;
        }
    }
}