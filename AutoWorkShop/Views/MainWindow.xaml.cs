using System.Windows;

namespace AutoWorkshop.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UpdateStatusBar();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите выйти из системы?",
                "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                var loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            }
        }

        private void UpdateStatusBar()
        {
            StatusBarText.Text = $"Готов к работе | Пользователь: Администратор | {DateTime.Now:dd.MM.yyyy HH:mm}";
        }
    }
}