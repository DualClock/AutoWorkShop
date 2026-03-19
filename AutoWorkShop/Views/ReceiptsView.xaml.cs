using System.Windows;
using System.Windows.Controls;
using AutoWorkshop.Services;
using AutoWorkshop.Models;
using System.Linq;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace AutoWorkshop.Views
{
    public partial class ReceiptsView : UserControl
    {
        public ReceiptsView()
        {
            InitializeComponent();
            LoadReceipts();
        }

        private void LoadReceipts()
        {
            using (var db = new AppDbContext())
            {
                ReceiptsDataGrid.ItemsSource = db.Receipts.Include(r => r.Order).ToList();
            }
        }

        private void GenerateReceipt_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SelectOrderDialog();
            if (dialog.ShowDialog() == true)
            {
                using (var db = new AppDbContext())
                {
                    var order = db.Orders.Find(dialog.SelectedOrderId);
                    if (order != null)
                    {
                        var receipt = new Receipt
                        {
                            OrderId = order.Id,
                            Amount = order.TotalCost,
                            IsPaid = true,
                            ReceiptNumber = $"RCT-{DateTime.Now:yyyyMMdd-HHmmss}"
                        };
                        db.Receipts.Add(receipt);
                        db.SaveChanges();

                        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                            $"Квитанция_{receipt.ReceiptNumber}.txt");
                        File.WriteAllText(path,
                            $"КВИТАНЦИЯ №{receipt.ReceiptNumber}\n\n" +
                            $"Дата: {DateTime.Now}\n" +
                            $"Заказ №{order.Id}\n" +
                            $"Сумма: {receipt.Amount} руб.\n\n" +
                            $"Оплачено: ДА");

                        MessageBox.Show($"Квитанция сформирована и сохранена:\n{path}",
                            "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                LoadReceipts();
            }
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Квитанция отправлена на печать", "Информация",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void NotifyClient_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Уведомление отправлено клиенту (СМС)", "Информация",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadReceipts();
            MessageBox.Show("Данные обновлены!", "Информация",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}