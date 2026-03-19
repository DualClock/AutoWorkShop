using System.Windows;
using System.Windows.Controls;
using AutoWorkshop.Services;
using AutoWorkshop.Models;
using System.Linq;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore;

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

        private void EditPart_Click(object sender, RoutedEventArgs e)
        {
            if (PartsDataGrid.SelectedItem is Part part)
            {
                var dialog = new EditPartDialog(part);
                if (dialog.ShowDialog() == true)
                {
                    LoadParts();
                    MessageBox.Show("Данные запчасти обновлены!", "Информация",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Выберите запчасть для редактирования!", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
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

        private void DeletePart_Click(object sender, RoutedEventArgs e)
        {
            if (PartsDataGrid.SelectedItem is Part part)
            {
                var result = MessageBox.Show($"Удалить запчасть {part.Name}?",
                    "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    using (var db = new AppDbContext())
                    {
                        var toDelete = db.Parts
                            .Include(p => p.OrderParts)
                            .FirstOrDefault(p => p.Id == part.Id);

                        if (toDelete != null)
                        {

                            if (toDelete.OrderParts != null && toDelete.OrderParts.Any())
                            {
                                MessageBox.Show($"Невозможно удалить запчасть: она используется в заказах ({toDelete.OrderParts.Count()}).",
                                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }

                            db.Parts.Remove(toDelete);
                            db.SaveChanges();
                            LoadParts();
                            MessageBox.Show("Запчасть удалена!", "Информация",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите запчасть для удаления!", "Предупреждение",
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

            using (var db = new AppDbContext())
            {
                var parts = db.Parts.ToList();
                var report = new StringBuilder();
                report.AppendLine("ОТЧЁТ ПО СКЛАДУ ЗАПЧАСТЕЙ");
                report.AppendLine("==========================");
                report.AppendLine($"Дата формирования: {DateTime.Now:dd.MM.yyyy HH:mm}");
                report.AppendLine();
                report.AppendLine($"Всего позиций: {parts.Count}");
                report.AppendLine($"Общая стоимость: {parts.Sum(p => p.Price * p.Quantity):F2} руб.");
                report.AppendLine();
                report.AppendLine("СПИСОК ЗАПЧАСТЕЙ:");
                report.AppendLine("-----------------");

                foreach (var p in parts)
                {
                    report.AppendLine($"{p.Name} (арт. {p.Article}) - {p.Quantity} шт. x {p.Price:F2} руб. = {p.Price * p.Quantity:F2} руб.");
                }

                File.WriteAllText(path, report.ToString(), Encoding.UTF8);
            }

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