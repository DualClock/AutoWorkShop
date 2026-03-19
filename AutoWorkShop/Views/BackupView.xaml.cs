using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore;
using AutoWorkshop.Services;
using AutoWorkshop.Models;
using System.Linq;
using Microsoft.Win32;

namespace AutoWorkshop.Views
{
    public partial class BackupView : UserControl
    {
        public BackupView()
        {
            InitializeComponent();
        }

        private void CreateBackup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "AutoWorkShopBackups");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string path = Path.Combine(folder, $"Backup_AutoWorkshop_{DateTime.Now:yyyyMMdd_HHmmss}.sql");

                using (var db = new AppDbContext())
                {
                    var backup = new StringBuilder();
                    backup.AppendLine("-- Резервная копия БД AutoWorkshopDB");
                    backup.AppendLine($"-- Дата создания: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
                    backup.AppendLine();


                    var clients = db.Clients.ToList();
                    backup.AppendLine($"-- Клиенты: {clients.Count} записей");
                    foreach (var c in clients)
                    {
                        backup.AppendLine($"INSERT INTO Clients (Id, FullName, Phone, Email, Address, CreatedDate) VALUES ({c.Id}, '{EscapeSql(c.FullName)}', '{EscapeSql(c.Phone)}', '{EscapeSql(c.Email)}', '{EscapeSql(c.Address)}', '{c.CreatedDate:yyyy-MM-dd HH:mm:ss}');");
                    }
                    backup.AppendLine();


                    var cars = db.Cars.ToList();
                    backup.AppendLine($"-- Автомобили: {cars.Count} записей");
                    foreach (var car in cars)
                    {
                        var yearStr = car.Year.HasValue ? car.Year.Value.ToString() : "NULL";
                        backup.AppendLine($"INSERT INTO Cars (Id, ClientId, Brand, Model, VIN, PlateNumber, Year) VALUES ({car.Id}, {car.ClientId}, '{EscapeSql(car.Brand)}', '{EscapeSql(car.Model)}', '{EscapeSql(car.VIN)}', '{EscapeSql(car.PlateNumber)}', {yearStr});");
                    }
                    backup.AppendLine();


                    var orders = db.Orders.ToList();
                    backup.AppendLine($"-- Заказы: {orders.Count} записей");
                    foreach (var o in orders)
                    {
                        var completedDateStr = o.CompletedDate != null ? $"'{o.CompletedDate:yyyy-MM-dd HH:mm:ss}'" : "NULL";
                        backup.AppendLine($"INSERT INTO Orders (Id, CarId, EmployeeId, Description, Status, TotalCost, CreatedDate, CompletedDate) VALUES ({o.Id}, {o.CarId}, {o.EmployeeId}, '{EscapeSql(o.Description)}', '{EscapeSql(o.Status)}', {o.TotalCost}, '{o.CreatedDate:yyyy-MM-dd HH:mm:ss}', {completedDateStr});");
                    }
                    backup.AppendLine();


                    var parts = db.Parts.ToList();
                    backup.AppendLine($"-- Запчасти: {parts.Count} записей");
                    foreach (var p in parts)
                    {
                        backup.AppendLine($"INSERT INTO Parts (Id, Name, Article, Quantity, Price, MinQuantity) VALUES ({p.Id}, '{EscapeSql(p.Name)}', '{EscapeSql(p.Article)}', {p.Quantity}, {p.Price}, {p.MinQuantity});");
                    }
                    backup.AppendLine();


                    var employees = db.Employees.ToList();
                    backup.AppendLine($"-- Сотрудники: {employees.Count} записей");
                    foreach (var emp in employees)
                    {
                        var deptIdStr = emp.DepartmentId.HasValue ? emp.DepartmentId.Value.ToString() : "NULL";
                        backup.AppendLine($"INSERT INTO Employees (Id, FullName, Position, Phone, Email, DepartmentId) VALUES ({emp.Id}, '{EscapeSql(emp.FullName)}', '{EscapeSql(emp.Position)}', '{EscapeSql(emp.Phone)}', '{EscapeSql(emp.Email)}', {deptIdStr});");
                    }
                    backup.AppendLine();


                    var departments = db.Departments.ToList();
                    backup.AppendLine($"-- Отделы: {departments.Count} записей");
                    foreach (var d in departments)
                    {
                        backup.AppendLine($"INSERT INTO Departments (Id, Name) VALUES ({d.Id}, '{EscapeSql(d.Name)}');");
                    }
                    backup.AppendLine();

                    File.WriteAllText(path, backup.ToString());
                }

                StatusText.Text = $"Копия создана: {path}";
                MessageBox.Show($"Резервная копия создана:\n{path}", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                StatusText.Text = "Ошибка при создании копии";
                MessageBox.Show($"Ошибка при создании резервной копии:\n{ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RestoreBackup_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "SQL файлы (*.sql)|*.sql|Все файлы (*.*)|*.*",
                Title = "Выберите файл резервной копии"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                MessageBox.Show($"Восстановление из резервной копии требует прямого доступа к SQL Server.\n\nФайл: {openFileDialog.FileName}\n\nДля восстановления используйте SQL Server Management Studio или выполните SQL скрипт вручную.",
                    "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private string EscapeSql(string value)
        {
            if (string.IsNullOrEmpty(value))
                return "";
            return value.Replace("'", "''");
        }
    }
}