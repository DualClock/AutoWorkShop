using System.Windows;
using System.Windows.Controls;
using AutoWorkshop.Services;
using AutoWorkshop.Models;
using System.Linq;

namespace AutoWorkshop.Views
{
    public partial class AddUserDialog : Window
    {
        private readonly User? _editingUser;

        public AddUserDialog(User? user = null)
        {
            InitializeComponent();
            _editingUser = user;

            LoadEmployees();

            if (_editingUser != null)
            {
                Title = "Редактирование пользователя";
                LoginTextBox.Text = _editingUser.Login;


                SetComboBoxSelectedIndex(RoleComboBox, _editingUser.Role);


                if (_editingUser.EmployeeId.HasValue)
                {
                    SetComboBoxSelectedIndex(EmployeeComboBox, _editingUser.EmployeeId.Value.ToString());
                }

                PasswordBox.Password = "********";
            }
            else
            {
                Title = "Добавление пользователя";

                RoleComboBox.SelectedIndex = 2;
                EmployeeComboBox.SelectedIndex = 0;
            }
        }

        private void SetComboBoxSelectedIndex(ComboBox comboBox, string tagValue)
        {
            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                if (comboBox.Items[i] is ComboBoxItem item &&
                    item.Tag?.ToString() == tagValue)
                {
                    comboBox.SelectedIndex = i;
                    break;
                }
            }
        }

        private void LoadEmployees()
        {
            using (var db = new AppDbContext())
            {
                var employees = db.Employees.ToList();

                EmployeeComboBox.Items.Clear();
                EmployeeComboBox.Items.Add(new ComboBoxItem { Content = "Не назначен", Tag = "" });

                foreach (var emp in employees)
                {
                    EmployeeComboBox.Items.Add(new ComboBoxItem
                    {
                        Content = emp.FullName,
                        Tag = emp.Id.ToString()
                    });
                }

                if (_editingUser == null)
                    EmployeeComboBox.SelectedIndex = 0;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoginTextBox.Text))
            {
                MessageBox.Show("Введите логин!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_editingUser == null && string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                MessageBox.Show("Введите пароль!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (RoleComboBox.SelectedItem is not ComboBoxItem selectedRoleItem ||
                selectedRoleItem.Tag == null)
            {
                MessageBox.Show("Выберите роль!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (var db = new AppDbContext())
                {
                    if (_editingUser != null)
                    {

                        _editingUser.Login = LoginTextBox.Text.Trim();
                        _editingUser.Role = selectedRoleItem.Tag.ToString()!;


                        if (!string.IsNullOrWhiteSpace(PasswordBox.Password) &&
                            PasswordBox.Password != "********")
                        {
                            _editingUser.PasswordHash = PasswordHelper.HashPassword(PasswordBox.Password);
                        }


                        if (EmployeeComboBox.SelectedItem is ComboBoxItem empItem &&
                            !string.IsNullOrEmpty(empItem.Tag?.ToString()))
                        {
                            _editingUser.EmployeeId = int.Parse(empItem.Tag.ToString()!);
                        }
                        else
                        {
                            _editingUser.EmployeeId = null;
                        }

                        db.Users.Update(_editingUser);
                    }
                    else
                    {


                        if (db.Users.Any(u => u.Login == LoginTextBox.Text.Trim()))
                        {
                            MessageBox.Show("Пользователь с таким логином уже существует!", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        var newUser = new User
                        {
                            Login = LoginTextBox.Text.Trim(),
                            PasswordHash = PasswordHelper.HashPassword(PasswordBox.Password),
                            Role = selectedRoleItem.Tag.ToString()!,
                            IsActive = true
                        };


                        if (EmployeeComboBox.SelectedItem is ComboBoxItem empItem &&
                            !string.IsNullOrEmpty(empItem.Tag?.ToString()))
                        {
                            newUser.EmployeeId = int.Parse(empItem.Tag.ToString()!);
                        }

                        db.Users.Add(newUser);
                    }

                    db.SaveChanges();
                }

                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
