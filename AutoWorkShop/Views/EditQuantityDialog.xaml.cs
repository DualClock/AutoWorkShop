using System.Windows;
using AutoWorkshop.Services;
using AutoWorkshop.Models;

namespace AutoWorkshop.Views
{
    public partial class EditQuantityDialog : Window
    {
        private Part _part;

        public EditQuantityDialog(Part part)
        {
            InitializeComponent();
            _part = part;
            PartNameText.Text = part.Name;
            QuantityTextBox.Text = part.Quantity.ToString();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(QuantityTextBox.Text, out var quantity))
            {
                using (var db = new AppDbContext())
                {
                    var p = db.Parts.Find(_part.Id);
                    if (p != null)
                    {
                        p.Quantity = quantity;
                        db.SaveChanges();
                    }
                }
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Введите корректное число!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}