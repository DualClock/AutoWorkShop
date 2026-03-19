using System.Windows;
using System.Windows.Controls;
using AutoWorkshop.Services;
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

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadOrders();
            MessageBox.Show("Данные обновлены!", "Информация",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}