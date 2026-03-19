using System.Windows;
using AutoWorkshop.Services;
using AutoWorkshop.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoWorkshop.Views
{
    public partial class AddDepartmentDialog : Window
    {
        private Department? _existingDepartment;

        public AddDepartmentDialog()
        {
            InitializeComponent();
        }

        public AddDepartmentDialog(Department department) : this()
        {
            _existingDepartment = department;
            NameTextBox.Text = department.Name;
            Title = "Редактирование отдела";
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show("Введите название отдела!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var db = new AppDbContext())
            {
                if (_existingDepartment == null)
                {
                    var department = new Department
                    {
                        Name = NameTextBox.Text
                    };
                    db.Departments.Add(department);
                }
                else
                {
                    var existing = db.Departments.Find(_existingDepartment.Id);
                    if (existing != null)
                    {
                        existing.Name = NameTextBox.Text;
                        db.Departments.Update(existing);
                    }
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
