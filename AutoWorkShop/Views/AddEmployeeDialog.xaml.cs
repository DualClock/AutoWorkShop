using System.Windows;
using AutoWorkshop.Services;
using AutoWorkshop.Models;
using System.Linq;

namespace AutoWorkshop.Views
{
    public partial class AddEmployeeDialog : Window
    {
        private Employee? _existingEmployee;

        public AddEmployeeDialog()
        {
            InitializeComponent();
            LoadDepartments();
        }

        public AddEmployeeDialog(Employee employee) : this()
        {
            _existingEmployee = employee;
            FullNameTextBox.Text = employee.FullName;
            PositionTextBox.Text = employee.Position;
            PhoneTextBox.Text = employee.Phone;
            EmailTextBox.Text = employee.Email;

            using (var db = new AppDbContext())
            {
                var dept = db.Departments.Find(employee.DepartmentId);
                if (dept != null)
                    DepartmentComboBox.SelectedItem = dept;
            }

            Title = "Редактирование сотрудника";
        }

        private void LoadDepartments()
        {
            using (var db = new AppDbContext())
            {
                DepartmentComboBox.ItemsSource = db.Departments.ToList();
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FullNameTextBox.Text))
            {
                MessageBox.Show("Введите ФИО!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var db = new AppDbContext())
            {
                if (_existingEmployee == null)
                {
                    var employee = new Employee
                    {
                        FullName = FullNameTextBox.Text,
                        Position = PositionTextBox.Text,
                        Phone = PhoneTextBox.Text,
                        Email = EmailTextBox.Text,
                        DepartmentId = (DepartmentComboBox.SelectedItem as Department)?.Id
                    };
                    db.Employees.Add(employee);
                }
                else
                {
                    _existingEmployee.FullName = FullNameTextBox.Text;
                    _existingEmployee.Position = PositionTextBox.Text;
                    _existingEmployee.Phone = PhoneTextBox.Text;
                    _existingEmployee.Email = EmailTextBox.Text;
                    _existingEmployee.DepartmentId = (DepartmentComboBox.SelectedItem as Department)?.Id;
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