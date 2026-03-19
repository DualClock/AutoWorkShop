using System.Windows;
using System.Windows.Controls;
using AutoWorkshop.Services;
using AutoWorkshop.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AutoWorkshop.Views
{
    public partial class OrdersView : UserControl
    {
        public OrdersView()
        {
            InitializeComponent();
            LoadOrders();
        }

        private void LoadOrders()
        {
            using (var db = new AppDbContext())
            {
                OrdersDataGrid.ItemsSource = db.Orders
                    .Include(o => o.Car)
                    .ThenInclude(c => c.Client)
                    .Include(o => o.Employee)
                    .ToList();
            }
        }

        private void NewOrder_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddOrderDialog();
            if (dialog.ShowDialog() == true)
            {
                LoadOrders();
                MessageBox.Show("Заказ создан!", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void EditOrder_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersDataGrid.SelectedItem is Order order)
            {
                var dialog = new EditOrderDialog(order);
                if (dialog.ShowDialog() == true)
                {
                    LoadOrders();
                    MessageBox.Show("Данные заказа обновлены!", "Информация",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Выберите заказ для редактирования!", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersDataGrid.SelectedItem is Order order)
            {
                var result = MessageBox.Show($"Удалить заказ #{order.Id}?",
                    "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    using (var db = new AppDbContext())
                    {
                        var toDelete = db.Orders
                            .Include(o => o.OrderParts)
                            .Include(o => o.Receipts)
                            .FirstOrDefault(o => o.Id == order.Id);

                        if (toDelete != null)
                        {

                            if (toDelete.OrderParts != null && toDelete.OrderParts.Any())
                            {
                                MessageBox.Show($"Невозможно удалить заказ: в нём есть запчасти ({toDelete.OrderParts.Count()}). Сначала удалите запчасти.",
                                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }


                            if (toDelete.Receipts != null && toDelete.Receipts.Any())
                            {
                                MessageBox.Show($"Невозможно удалить заказ: есть квитанции об оплате ({toDelete.Receipts.Count()}). Сначала удалите квитанции.",
                                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }

                            db.Orders.Remove(toDelete);
                            db.SaveChanges();
                            LoadOrders();
                            MessageBox.Show("Заказ удалён!", "Информация",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите заказ для удаления!", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadOrders();
            MessageBox.Show("Данные обновлены!", "Информация",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}