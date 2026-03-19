using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AutoWorkshop.Services;
using AutoWorkshop.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoWorkshop.Views
{
    public partial class EditOrderDialog : Window
    {
        private readonly Order _order;

        public EditOrderDialog(Order order)
        {
            InitializeComponent();
            _order = order;
            LoadComboBoxes();
            LoadOrderData();
        }

        private void LoadComboBoxes()
        {
            using (var db = new AppDbContext())
            {

                CarComboBox.ItemsSource = db.Cars
                    .Include(c => c.Client)
                    .ToList();


                EmployeeComboBox.ItemsSource = db.Employees.ToList();
            }
        }

        private void LoadOrderData()
        {

            CarComboBox.SelectedItem = CarComboBox.ItemsSource
                .Cast<Car>()
                .FirstOrDefault(c => c.Id == _order.CarId);

            EmployeeComboBox.SelectedItem = EmployeeComboBox.ItemsSource
                .Cast<Employee>()
                .FirstOrDefault(e => e.Id == _order.EmployeeId);

            DescriptionTextBox.Text = _order.Description;
            CostTextBox.Text = _order.TotalCost.ToString();


            switch (_order.Status)
            {
                case "Новый":
                    StatusComboBox.SelectedIndex = 0;
                    break;
                case "В работе":
                    StatusComboBox.SelectedIndex = 1;
                    break;
                case "Готов":
                    StatusComboBox.SelectedIndex = 2;
                    break;
                case "Закрыт":
                    StatusComboBox.SelectedIndex = 3;
                    break;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
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

            if (StatusComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите статус!", "Ошибка",
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
                var existing = db.Orders.Find(_order.Id);
                if (existing != null)
                {
                    existing.CarId = ((Car)CarComboBox.SelectedItem).Id;
                    existing.EmployeeId = ((Employee)EmployeeComboBox.SelectedItem).Id;
                    existing.Description = DescriptionTextBox.Text;
                    existing.Status = StatusComboBox.Text;
                    existing.TotalCost = cost;


                    if (existing.Status == "Готов" || existing.Status == "Закрыт")
                    {
                        if (existing.CompletedDate == null)
                            existing.CompletedDate = DateTime.Now;
                    }

                    db.Orders.Update(existing);
                    db.SaveChanges();
                }
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
