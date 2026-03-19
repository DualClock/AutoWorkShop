using System.Windows;
using AutoWorkshop.Services;
using System.Linq;

namespace AutoWorkshop.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string login = LoginTextBox.Text;
                string password = PasswordBox.Password;

                if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
                {
                    ErrorText.Text = "Введите логин и пароль";
                    return;
                }

                using (var db = new AppDbContext())
                {
                    // Проверяем подключение к БД
                    var canConnect = db.Database.CanConnect();
                    if (!canConnect)
                    {
                        ErrorText.Text = "Нет подключения к базе данных!";
                        MessageBox.Show("Ошибка: Не удалось подключиться к SQL Server.\n\nПроверьте:\n1. Запущена ли служба SQL Server\n2. Правильность строки подключения",
                            "Ошибка подключения", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    var user = db.Users.FirstOrDefault(u => u.Login == login && u.PasswordHash == password);

                    if (user != null)
                    {
                        if (!user.IsActive)
                        {
                            ErrorText.Text = "Учётная запись заблокирована";
                            return;
                        }

                        var mainWindow = new MainWindow();
                        mainWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        ErrorText.Text = "Неверный логин или пароль";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorText.Text = "Ошибка: " + ex.Message;
                MessageBox.Show($"Произошла ошибка:\n\n{ex.Message}\n\n{ex.InnerException?.Message}",
                    "Ошибка приложения", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}