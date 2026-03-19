using System.Windows;
using AutoWorkshop.Services;
using AutoWorkshop.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AutoWorkshop.Views
{
    public partial class AddCarDialog : Window
    {
        private Car? _existingCar;

        public AddCarDialog()
        {
            InitializeComponent();
            LoadClients();
        }

        public AddCarDialog(Car car) : this()
        {
            _existingCar = car;
            BrandTextBox.Text = car.Brand;
            ModelTextBox.Text = car.Model;
            VinTextBox.Text = car.VIN;
            PlateNumberTextBox.Text = car.PlateNumber;
            YearTextBox.Text = car.Year?.ToString();

            using (var db = new AppDbContext())
            {
                var client = db.Clients.Find(car.ClientId);
                if (client != null)
                    ClientComboBox.SelectedItem = client;
            }

            Title = "Редактирование автомобиля";
        }

        private void LoadClients()
        {
            using (var db = new AppDbContext())
            {
                ClientComboBox.ItemsSource = db.Clients.ToList();
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (ClientComboBox.SelectedItem is not Client client)
            {
                MessageBox.Show("Выберите клиента!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var db = new AppDbContext())
            {
                if (_existingCar == null)
                {

                    var car = new Car
                    {
                        ClientId = client.Id,
                        Brand = BrandTextBox.Text,
                        Model = ModelTextBox.Text,
                        VIN = VinTextBox.Text,
                        PlateNumber = PlateNumberTextBox.Text,
                        Year = int.TryParse(YearTextBox.Text, out var y) ? y : null
                    };
                    db.Cars.Add(car);
                }
                else
                {

                    var existing = db.Cars.Find(_existingCar.Id);
                    if (existing != null)
                    {
                        existing.ClientId = client.Id;
                        existing.Brand = BrandTextBox.Text;
                        existing.Model = ModelTextBox.Text;
                        existing.VIN = VinTextBox.Text;
                        existing.PlateNumber = PlateNumberTextBox.Text;
                        existing.Year = int.TryParse(YearTextBox.Text, out var y) ? y : null;
                        db.Cars.Update(existing);
                    }
                }
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