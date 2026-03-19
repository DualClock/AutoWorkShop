using AutoWorkshop.Models;
using System.Linq;

namespace AutoWorkshop.Services
{
    public static class UserRoles
    {
        public const string Admin = "Admin";
        public const string Manager = "Manager";
        public const string Worker = "Worker";
    }

    public class UserService
    {
        private static User? _currentUser;

        public static User? CurrentUser => _currentUser;

        public static bool IsLoggedIn => _currentUser != null;

        public static bool IsAdmin => _currentUser?.Role == UserRoles.Admin;

        public static bool IsManager => _currentUser?.Role == UserRoles.Admin || _currentUser?.Role == UserRoles.Manager;

        public static void SetCurrentUser(User user)
        {
            _currentUser = user;
        }

        public static void Logout()
        {
            _currentUser = null;
        }

        public static bool CanManageUsers()
        {
            return _currentUser?.Role == UserRoles.Admin;
        }

        public static bool CanEditData()
        {
            return _currentUser?.Role == UserRoles.Admin || _currentUser?.Role == UserRoles.Manager;
        }

        public static bool CanDeleteData()
        {
            return _currentUser?.Role == UserRoles.Admin;
        }

        public static string GetRoleName(string role)
        {
            return role switch
            {
                UserRoles.Admin => "Администратор",
                UserRoles.Manager => "Менеджер",
                UserRoles.Worker => "Рабочий",
                _ => role
            };
        }
    }
}
