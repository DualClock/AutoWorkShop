using System.Windows;
using AutoWorkshop.Services;
using AutoWorkshop.Models;

namespace AutoWorkshop.Views
{
    public partial class AddPartDialog : Window
    {
        public AddPartDialog()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show("Введите наименование!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var db = new AppDbContext())
            {
                var part = new Part
                {
                    Name = NameTextBox.Text,
                    Article = ArticleTextBox.Text,
                    Quantity = int.TryParse(QuantityTextBox.Text, out var q) ? q : 0,
                    Price = decimal.TryParse(PriceTextBox.Text, out var p) ? p : 0,
                    MinQuantity = int.TryParse(MinQuantityTextBox.Text, out var m) ? m : 5
                };
                db.Parts.Add(part);
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