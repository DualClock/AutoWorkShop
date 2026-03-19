using System.Windows;
using System.Windows.Controls;
using AutoWorkshop.Services;
using AutoWorkshop.Models;
using System.Linq;

namespace AutoWorkshop.Views
{
    public partial class NotificationsView : UserControl
    {
        public NotificationsView()
        {
            InitializeComponent();
            LoadNotifications();
        }

        private void LoadNotifications()
        {
            using (var db = new AppDbContext())
            {
                NotificationsDataGrid.ItemsSource = db.Notifications.ToList();
            }
        }

        private void NewNotification_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Новое уведомление создано", "Информация",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void MarkAllRead_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new AppDbContext())
            {
                foreach (var n in db.Notifications.Where(n => !n.IsRead))
                {
                    n.IsRead = true;
                }
                db.SaveChanges();
            }
            LoadNotifications();
            MessageBox.Show("Все уведомления отмечены как прочитанные", "Информация",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}