using AutoWorkshop.Models;
using AutoWorkshop.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;

namespace AutoWorkshop.Views
{
    public partial class SelectOrderDialog : Window
    {
        public int SelectedOrderId { get; private set; }

        public SelectOrderDialog()
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

        private void Select_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersDataGrid.SelectedItem is Order order)
            {
                SelectedOrderId = order.Id;
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Выберите заказ!", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}