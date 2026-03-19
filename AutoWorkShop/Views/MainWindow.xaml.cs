using System.Windows;
using AutoWorkshop.Services;

namespace AutoWorkshop.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UpdateUserInfo();
            UpdateStatusBar();
        }

        private void UpdateUserInfo()
        {
            if (UserService.CurrentUser != null)
            {
                var roleName = UserService.GetRoleName(UserService.CurrentUser.Role);
                UserNameText.Text = $"{UserService.CurrentUser.Login} ({roleName})";
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите выйти из системы?",
                "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                UserService.Logout();
                var loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            }
        }

        private void UpdateStatusBar()
        {
            StatusBarText.Text = $"Готов к работе | Пользователь: {UserNameText.Text} | {DateTime.Now:dd.MM.yyyy HH:mm}";
        }
    }
}