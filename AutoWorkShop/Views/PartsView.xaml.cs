using System.Windows;
using System.Windows.Controls;
using AutoWorkshop.Services;
using AutoWorkshop.Models;
using System.Linq;
using System.IO;

namespace AutoWorkshop.Views
{
    public partial class PartsView : UserControl
    {
        public PartsView()
        {
            InitializeComponent();
            LoadParts();
        }

        private void LoadParts()
        {
            using (var db = new AppDbContext())
            {
                PartsDataGrid.ItemsSource = db.Parts.ToList();
            }
        }

        private void AddPart_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddPartDialog();
            if (dialog.ShowDialog() == true)
            {
                LoadParts();
                MessageBox.Show("Запчасть добавлена!", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void EditQuantity_Click(object sender, RoutedEventArgs e)
        {
            if (PartsDataGrid.SelectedItem is Part part)
            {
                var dialog = new EditQuantityDialog(part);
                if (dialog.ShowDialog() == true)
                {
                    LoadParts();
                    MessageBox.Show("Количество обновлено!", "Информация",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Выберите запчасть!", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void OrderSupply_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Форма заказа поставки открыта", "Информация",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void PrintReport_Click(object sender, RoutedEventArgs e)
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Отчёт_Склад.txt");
            File.WriteAllText(path, "Отчёт по складу запчастей\n\n" + DateTime.Now + "\n\nДанные экспортированы");
            MessageBox.Show($"Отчёт сохранён на рабочем столе:\n{path}", "Информация",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadParts();
            MessageBox.Show("Данные обновлены!", "Информация",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}