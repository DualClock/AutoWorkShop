using System.Windows;
using System.Windows.Controls;
using AutoWorkshop.Services;
using AutoWorkshop.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AutoWorkshop.Views
{
    public partial class CarsView : UserControl
    {
        public CarsView()
        {
            InitializeComponent();
            LoadCars();
        }

        private void LoadCars()
        {
            using (var db = new AppDbContext())
            {
                CarsDataGrid.ItemsSource = db.Cars.Include(c => c.Client).ToList();
            }
        }

        private void AddCar_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddCarDialog();
            if (dialog.ShowDialog() == true)
            {
                LoadCars();
                MessageBox.Show("Автомобиль добавлен!", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void DeleteCar_Click(object sender, RoutedEventArgs e)
        {
            if (CarsDataGrid.SelectedItem is Car car)
            {
                var result = MessageBox.Show($"Удалить автомобиль {car.Brand} {car.Model}?",
                    "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    using (var db = new AppDbContext())
                    {
                        var toDelete = db.Cars.Find(car.Id);
                        if (toDelete != null)
                        {
                            db.Cars.Remove(toDelete);
                            db.SaveChanges();
                            LoadCars();
                            MessageBox.Show("Автомобиль удалён!", "Информация",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите автомобиль!", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadCars();
            MessageBox.Show("Данные обновлены!", "Информация",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}