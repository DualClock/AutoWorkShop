using System;
using System.Windows;
using System.Windows.Controls;
using AutoWorkshop.Services;
using AutoWorkshop.Models;

namespace AutoWorkshop.Views
{
    public partial class NewNotificationDialog : Window
    {
        public NewNotificationDialog()
        {
            InitializeComponent();
            TypeComboBox.SelectedIndex = 0;
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MessageTextBox.Text))
            {
                MessageBox.Show("Введите текст уведомления!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var db = new AppDbContext())
            {
                var notification = new Notification
                {
                    Message = MessageTextBox.Text,
                    Type = ((ComboBoxItem)TypeComboBox.SelectedItem).Content.ToString(),
                    CreatedDate = DateTime.Now,
                    IsRead = false
                };

                db.Notifications.Add(notification);
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
