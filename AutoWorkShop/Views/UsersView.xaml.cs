using System.Windows;
using System.Windows.Controls;
using AutoWorkshop.Services;
using AutoWorkshop.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AutoWorkshop.Views
{
    public partial class UsersView : UserControl
    {
        public UsersView()
        {
            InitializeComponent();
            UpdateButtonsVisibility();
            LoadUsers();
        }

        private void UpdateButtonsVisibility()
        {

            System.Diagnostics.Debug.WriteLine($"CurrentUser: {UserService.CurrentUser?.Login}, Role: {UserService.CurrentUser?.Role}");
            System.Diagnostics.Debug.WriteLine($"CanManageUsers: {UserService.CanManageUsers()}");

            AddUserButton.Visibility = UserService.CanManageUsers() ? Visibility.Visible : Visibility.Collapsed;
            EditUserButton.Visibility = UserService.CanManageUsers() ? Visibility.Visible : Visibility.Collapsed;
            DeleteUserButton.Visibility = UserService.CanManageUsers() ? Visibility.Visible : Visibility.Collapsed;
            BlockUserButton.Visibility = UserService.CanManageUsers() ? Visibility.Visible : Visibility.Collapsed;
        }

        private void LoadUsers()
        {
            using (var db = new AppDbContext())
            {
                var users = db.Users
                    .Include(u => u.Employee)
                    .Select(u => new
                    {
                        u.Id,
                        u.Login,
                        u.Role,
                        RoleName = UserService.GetRoleName(u.Role),
                        EmployeeName = u.Employee != null ? u.Employee.FullName : "Не назначен",
                        StatusText = u.IsActive ? "Активен" : "Заблокирован",
                        u.LastLogin
                    })
                    .ToList();

                UsersDataGrid.ItemsSource = users;
            }
        }

        private void UsersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddUserDialog();
            if (dialog.ShowDialog() == true)
            {
                LoadUsers();
                MessageBox.Show("Пользователь добавлен!", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            if (UsersDataGrid.SelectedItem != null)
            {
                var selected = UsersDataGrid.SelectedItem as dynamic;
                if (selected != null)
                {
                    int userId = selected.Id;
                    using (var db = new AppDbContext())
                    {
                        var user = db.Users
                            .Include(u => u.Employee)
                            .FirstOrDefault(u => u.Id == userId);

                        if (user != null)
                        {
                            var dialog = new AddUserDialog(user);
                            if (dialog.ShowDialog() == true)
                            {
                                LoadUsers();
                                MessageBox.Show("Данные пользователя обновлены!", "Информация",
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите пользователя для редактирования!", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (UsersDataGrid.SelectedItem != null)
            {
                var selected = UsersDataGrid.SelectedItem as dynamic;
                if (selected != null)
                {
                    int userId = selected.Id;


                    if (UserService.CurrentUser != null && UserService.CurrentUser.Id == userId)
                    {
                        MessageBox.Show("Нельзя удалить самого себя!", "Предупреждение",
                            MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    var result = MessageBox.Show($"Удалить пользователя {selected.Login}?",
                        "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        using (var db = new AppDbContext())
                        {
                            var user = db.Users.FirstOrDefault(u => u.Id == userId);
                            if (user != null)
                            {
                                db.Users.Remove(user);
                                db.SaveChanges();
                                LoadUsers();
                                MessageBox.Show("Пользователь удалён!", "Информация",
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите пользователя для удаления!", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BlockUser_Click(object sender, RoutedEventArgs e)
        {
            if (UsersDataGrid.SelectedItem != null)
            {
                var selected = UsersDataGrid.SelectedItem as dynamic;
                if (selected != null)
                {
                    int userId = selected.Id;


                    if (UserService.CurrentUser != null && UserService.CurrentUser.Id == userId)
                    {
                        MessageBox.Show("Нельзя заблокировать самого себя!", "Предупреждение",
                            MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    using (var db = new AppDbContext())
                    {
                        var user = db.Users.FirstOrDefault(u => u.Id == userId);
                        if (user != null)
                        {
                            user.IsActive = !user.IsActive;
                            db.SaveChanges();
                            LoadUsers();

                            string action = user.IsActive ? "разблокирован" : "заблокирован";
                            MessageBox.Show($"Пользователь {user.Login} {action}!", "Информация",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите пользователя для блокировки!", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
