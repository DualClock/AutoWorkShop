using System.Windows.Controls;
using AutoWorkshop.Services;
using System.Linq;

namespace AutoWorkshop.Views
{
    public partial class DepartmentsView : UserControl
    {
        public DepartmentsView()
        {
            InitializeComponent();
            LoadDepartments();
        }

        private void LoadDepartments()
        {
            using (var db = new AppDbContext())
            {
                DepartmentsListBox.ItemsSource = db.Departments.ToList();
            }
        }

        private void DepartmentsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DepartmentsListBox.SelectedItem is Models.Department department)
            {
                using (var db = new AppDbContext())
                {
                    EmployeesDataGrid.ItemsSource = db.Employees
                        .Where(emp => emp.DepartmentId == department.Id)
                        .ToList();
                }
            }
        }
    }
}