using System;
using System.Windows;
using System.Windows.Controls;
using AutoWorkshop.Services;
using AutoWorkshop.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AutoWorkshop.Views
{
    public partial class AddOrderDialog : Window
    {
        public AddOrderDialog()
        {
            InitializeComponent();
            LoadComboBoxes();
        }

        private void LoadComboBoxes()
        {
            using (var db = new AppDbContext())
            {

                CarComboBox.ItemsSource = db.Cars
                    .Include(c => c.Client)
                    .ToList();


                EmployeeComboBox.ItemsSource = db.Employees.ToList();


                DepartmentComboBox.ItemsSource = db.Departments.ToList();
            }
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {

            if (CarComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите автомобиль!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (EmployeeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите сотрудника!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (DepartmentComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите отдел!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(DescriptionTextBox.Text))
            {
                MessageBox.Show("Введите описание проблемы!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!decimal.TryParse(CostTextBox.Text, out decimal cost) || cost < 0)
            {
                MessageBox.Show("Введите корректную стоимость!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var db = new AppDbContext())
            {
                var car = (Car)CarComboBox.SelectedItem;
                var employee = (Employee)EmployeeComboBox.SelectedItem;
                var department = (Department)DepartmentComboBox.SelectedItem;

                var order = new Order
                {
                    CarId = car.Id,
                    EmployeeId = employee.Id,
                    Description = DescriptionTextBox.Text,
                    Status = "Новый",
                    TotalCost = cost,
                    CreatedDate = DateTime.Now
                };

                db.Orders.Add(order);
                db.SaveChanges();
            }

            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
