using System;
using System.Windows;
using AutoWorkshop.Services;
using AutoWorkshop.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoWorkshop.Views
{
    public partial class EditPartDialog : Window
    {
        private readonly Part _part;

        public EditPartDialog(Part part)
        {
            InitializeComponent();
            _part = part;
            LoadPartData();
        }

        private void LoadPartData()
        {
            NameTextBox.Text = _part.Name;
            ArticleTextBox.Text = _part.Article;
            QuantityTextBox.Text = _part.Quantity.ToString();
            PriceTextBox.Text = _part.Price.ToString();
            MinQuantityTextBox.Text = _part.MinQuantity.ToString();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show("Введите наименование запчасти!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(ArticleTextBox.Text))
            {
                MessageBox.Show("Введите артикул!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(QuantityTextBox.Text, out int quantity) || quantity < 0)
            {
                MessageBox.Show("Введите корректное количество!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!decimal.TryParse(PriceTextBox.Text, out decimal price) || price < 0)
            {
                MessageBox.Show("Введите корректную цену!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(MinQuantityTextBox.Text, out int minQuantity) || minQuantity < 0)
            {
                MessageBox.Show("Введите корректный минимальный остаток!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var db = new AppDbContext())
            {
                var existing = db.Parts.Find(_part.Id);
                if (existing != null)
                {
                    existing.Name = NameTextBox.Text;
                    existing.Article = ArticleTextBox.Text;
                    existing.Quantity = quantity;
                    existing.Price = price;
                    existing.MinQuantity = minQuantity;

                    db.Parts.Update(existing);
                    db.SaveChanges();
                }
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
