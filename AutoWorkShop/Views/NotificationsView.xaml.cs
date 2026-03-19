using System;
using System.Windows;
using System.Windows.Controls;
using AutoWorkshop.Services;
using AutoWorkshop.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
                NotificationsDataGrid.ItemsSource = db.Notifications
                    .OrderByDescending(n => n.CreatedDate)
                    .ToList();
            }
        }

        private void NewNotification_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new NewNotificationDialog();
            if (dialog.ShowDialog() == true)
            {
                LoadNotifications();
                MessageBox.Show("Уведомление создано!", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void DeleteNotification_Click(object sender, RoutedEventArgs e)
        {
            if (NotificationsDataGrid.SelectedItem is Notification notification)
            {
                var result = MessageBox.Show($"Удалить уведомление?",
                    "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    using (var db = new AppDbContext())
                    {
                        var toDelete = db.Notifications.Find(notification.Id);
                        if (toDelete != null)
                        {
                            db.Notifications.Remove(toDelete);
                            db.SaveChanges();
                            LoadNotifications();
                            MessageBox.Show("Уведомление удалено!", "Информация",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите уведомление для удаления!", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
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