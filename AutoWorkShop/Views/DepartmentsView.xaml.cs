using System.Windows;
using System.Windows.Controls;
using AutoWorkshop.Services;
using AutoWorkshop.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AutoWorkshop.Views
{
    public partial class DepartmentsView : UserControl
    {
        public DepartmentsView()
        {
            InitializeComponent();
            UpdateButtonsVisibility();
            LoadDepartments();
        }

        private void UpdateButtonsVisibility()
        {
            AddDepartmentButton.Visibility = UserService.CanEditData() ? Visibility.Visible : Visibility.Collapsed;
            EditDepartmentButton.Visibility = UserService.CanEditData() ? Visibility.Visible : Visibility.Collapsed;
            DeleteDepartmentButton.Visibility = UserService.CanDeleteData() ? Visibility.Visible : Visibility.Collapsed;
        }

        private void LoadDepartments()
        {
            using (var db = new AppDbContext())
            {
                DepartmentsListBox.ItemsSource = db.Departments.ToList();
            }
        }

        private void DepartmentsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DepartmentsListBox.SelectedItem is Models.Department department)
            {
                using (var db = new AppDbContext())
                {
                    EmployeesDataGrid.ItemsSource = db.Employees
                        .Where(emp => emp.DepartmentId == department.Id)
                        .ToList();
                }
            }
        }

        private void AddDepartment_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddDepartmentDialog();
            if (dialog.ShowDialog() == true)
            {
                LoadDepartments();
                MessageBox.Show("Отдел добавлен!", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void EditDepartment_Click(object sender, RoutedEventArgs e)
        {
            if (DepartmentsListBox.SelectedItem is Models.Department department)
            {
                var dialog = new AddDepartmentDialog(department);
                if (dialog.ShowDialog() == true)
                {
                    LoadDepartments();
                    MessageBox.Show("Данные отдела обновлены!", "Информация",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Выберите отдел для редактирования!", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteDepartment_Click(object sender, RoutedEventArgs e)
        {
            if (DepartmentsListBox.SelectedItem is Models.Department department)
            {
                var result = MessageBox.Show($"Удалить отдел {department.Name}?",
                    "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    using (var db = new AppDbContext())
                    {
                        var toDelete = db.Departments
                            .Include(d => d.Employees)
                            .FirstOrDefault(d => d.Id == department.Id);

                        if (toDelete != null)
                        {

                            if (toDelete.Employees != null && toDelete.Employees.Any())
                            {
                                MessageBox.Show($"Невозможно удалить отдел: в нём есть сотрудники ({toDelete.Employees.Count()}). Сначала удалите или переместите сотрудников.",
                                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }

                            db.Departments.Remove(toDelete);
                            db.SaveChanges();
                            LoadDepartments();
                            MessageBox.Show("Отдел удалён!", "Информация",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите отдел для удаления!", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}