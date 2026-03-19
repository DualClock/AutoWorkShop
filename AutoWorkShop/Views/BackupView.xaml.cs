using System.Windows;
using System.Windows.Controls;
using System.IO;

namespace AutoWorkshop.Views
{
    public partial class BackupView : UserControl
    {
        public BackupView()
        {
            InitializeComponent();
        }

        private void CreateBackup_Click(object sender, RoutedEventArgs e)
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                $"Backup_AutoWorkshop_{DateTime.Now:yyyyMMdd_HHmmss}.sql");
            File.WriteAllText(path, "-- Резервная копия БД AutoWorkshopDB\n-- Дата: " + DateTime.Now);

            StatusText.Text = $"Копия создана: {path}";
            MessageBox.Show($"Резервная копия создана:\n{path}", "Информация",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void RestoreBackup_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Выберите файл резервной копии для восстановления", "Информация",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}