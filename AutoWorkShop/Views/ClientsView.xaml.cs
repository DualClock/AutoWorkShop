using System.Windows;
using System.Windows.Controls;
using AutoWorkshop.Services;
using AutoWorkshop.Models;
using System.Linq;

namespace AutoWorkshop.Views
{
    public partial class ClientsView : UserControl
    {
        public ClientsView()
        {
            InitializeComponent();
            LoadClients();
        }

        private void LoadClients()
        {
            using (var db = new AppDbContext())
            {
                ClientsDataGrid.ItemsSource = db.Clients.ToList();
            }
        }

        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddClientDialog();
            if (dialog.ShowDialog() == true)
            {
                LoadClients();
                MessageBox.Show("Клиент успешно добавлен!", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void EditClient_Click(object sender, RoutedEventArgs e)
        {
            if (ClientsDataGrid.SelectedItem is Client client)
            {
                var dialog = new AddClientDialog(client);
                if (dialog.ShowDialog() == true)
                {
                    LoadClients();
                    MessageBox.Show("Данные клиента обновлены!", "Информация",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Выберите клиента для редактирования!", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteClient_Click(object sender, RoutedEventArgs e)
        {
            if (ClientsDataGrid.SelectedItem is Client client)
            {
                var result = MessageBox.Show($"Удалить клиента {client.FullName}?",
                    "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    using (var db = new AppDbContext())
                    {
                        var toDelete = db.Clients.Find(client.Id);
                        if (toDelete != null)
                        {
                            db.Clients.Remove(toDelete);
                            db.SaveChanges();
                            LoadClients();
                            MessageBox.Show("Клиент удалён!", "Информация",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите клиента для удаления!", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadClients();
            MessageBox.Show("Данные обновлены!", "Информация",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}