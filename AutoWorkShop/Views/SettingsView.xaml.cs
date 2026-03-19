using System.Windows;
using System.Windows.Controls;
using AutoWorkshop.Services;
using System.Linq;

namespace AutoWorkshop.Views
{
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            using (var db = new AppDbContext())
            {
                var name = db.Settings.FirstOrDefault(s => s.KeyName == "WorkshopName");
                var address = db.Settings.FirstOrDefault(s => s.KeyName == "Address");
                var phone = db.Settings.FirstOrDefault(s => s.KeyName == "Phone");
                var email = db.Settings.FirstOrDefault(s => s.KeyName == "Email");

                if (name != null) NameTextBox.Text = name.Value;
                if (address != null) AddressTextBox.Text = address.Value;
                if (phone != null) PhoneTextBox.Text = phone.Value;
                if (email != null) EmailTextBox.Text = email.Value;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new AppDbContext())
            {
                SaveSetting(db, "WorkshopName", NameTextBox.Text);
                SaveSetting(db, "Address", AddressTextBox.Text);
                SaveSetting(db, "Phone", PhoneTextBox.Text);
                SaveSetting(db, "Email", EmailTextBox.Text);
                db.SaveChanges();
            }

            MessageBox.Show("Настройки сохранены!", "Информация",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SaveSetting(AppDbContext db, string key, string value)
        {
            var setting = db.Settings.FirstOrDefault(s => s.KeyName == key);
            if (setting == null)
            {
                db.Settings.Add(new Models.Setting { KeyName = key, Value = value });
            }
            else
            {
                setting.Value = value;
            }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            LoadSettings();
            MessageBox.Show("Настройки сброшены!", "Информация",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}