using Microsoft.EntityFrameworkCore;
using AutoWorkshop.Models;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace AutoWorkshop.Services
{
    public class AppDbContext : DbContext
    {
        private static readonly string ConnectionString;

        static AppDbContext()
        {
            // Читаем строку подключения из appsettings.json
            var basePath = Path.GetDirectoryName(System.AppContext.BaseDirectory) ?? Directory.GetCurrentDirectory();
            var configPath = Path.Combine(basePath, "appsettings.json");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                .Build();

            ConnectionString = configuration.GetConnectionString("DefaultConnection") 
                ?? "Data Source=(localdb)\\MSSQLLocalDB;Database=AutoWorkshopDB;Integrated Security=True;TrustServerCertificate=True;";
        }

        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderPart> OrderParts { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Setting> Settings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Employee)
                .WithOne(e => e.UserAccount)
                .HasForeignKey<User>(u => u.EmployeeId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
