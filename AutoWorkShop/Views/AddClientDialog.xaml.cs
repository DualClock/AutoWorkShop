using System.Windows;
using AutoWorkshop.Services;
using AutoWorkshop.Models;

namespace AutoWorkshop.Views
{
    public partial class AddClientDialog : Window
    {
        private Client? _existingClient;

        public AddClientDialog()
        {
            InitializeComponent();
        }

        public AddClientDialog(Client client) : this()
        {
            _existingClient = client;
            FullNameTextBox.Text = client.FullName;
            PhoneTextBox.Text = client.Phone;
            EmailTextBox.Text = client.Email;
            AddressTextBox.Text = client.Address;
            Title = "Редактирование клиента";
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FullNameTextBox.Text))
            {
                MessageBox.Show("Введите ФИО клиента!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var db = new AppDbContext())
            {
                if (_existingClient == null)
                {
                    // Новый клиент
                    var client = new Client
                    {
                        FullName = FullNameTextBox.Text,
                        Phone = PhoneTextBox.Text,
                        Email = EmailTextBox.Text,
                        Address = AddressTextBox.Text,
                        CreatedDate = DateTime.Now
                    };
                    db.Clients.Add(client);
                }
                else
                {
                    // Редактирование
                    _existingClient.FullName = FullNameTextBox.Text;
                    _existingClient.Phone = PhoneTextBox.Text;
                    _existingClient.Email = EmailTextBox.Text;
                    _existingClient.Address = AddressTextBox.Text;
                }
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