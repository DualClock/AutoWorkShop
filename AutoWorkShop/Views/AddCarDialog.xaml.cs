using System.Windows;
using AutoWorkshop.Services;
using AutoWorkshop.Models;
using System.Linq;

namespace AutoWorkshop.Views
{
    public partial class AddCarDialog : Window
    {
        public AddCarDialog()
        {
            InitializeComponent();
            LoadClients();
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