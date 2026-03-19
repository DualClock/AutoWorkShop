using System.Windows;
using System.Windows.Controls;
using AutoWorkshop.Services;
using AutoWorkshop.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AutoWorkshop.Views
{
    public partial class EmployeesView : UserControl
    {
        public EmployeesView()
        {
            InitializeComponent();
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            using (var db = new AppDbContext())
            {
                EmployeesDataGrid.ItemsSource = db.Employees.Include(e => e.Department).ToList();
            }
        }

        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddEmployeeDialog();
            if (dialog.ShowDialog() == true)
            {
                LoadEmployees();
                MessageBox.Show("Сотрудник добавлен!", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void EditEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeesDataGrid.SelectedItem is Employee employee)
            {
                var dialog = new AddEmployeeDialog(employee);
                if (dialog.ShowDialog() == true)
                {
                    LoadEmployees();
                    MessageBox.Show("Данные обновлены!", "Информация",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Выберите сотрудника!", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeesDataGrid.SelectedItem is Employee employee)
            {
                var result = MessageBox.Show($"Удалить сотрудника {employee.FullName}?",
                    "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    using (var db = new AppDbContext())
                    {
                        var toDelete = db.Employees
                            .Include(emp => emp.Orders)
                            .Include(emp => emp.UserAccount)
                            .FirstOrDefault(emp => emp.Id == employee.Id);

                        if (toDelete != null)
                        {

                            if (toDelete.Orders != null && toDelete.Orders.Any())
                            {
                                MessageBox.Show($"Невозможно удалить сотрудника: у него есть активные заказы ({toDelete.Orders.Count()}). Сначала удалите или переназначьте заказы.",
                                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }


                            if (toDelete.UserAccount != null)
                            {
                                MessageBox.Show($"Невозможно удалить сотрудника: у него есть учётная запись в системе ({toDelete.UserAccount.Login}). Сначала удалите учётную запись.",
                                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }

                            db.Employees.Remove(toDelete);
                            db.SaveChanges();
                            LoadEmployees();
                            MessageBox.Show("Сотрудник удалён!", "Информация",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите сотрудника!", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadEmployees();
            MessageBox.Show("Данные обновлены!", "Информация",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}