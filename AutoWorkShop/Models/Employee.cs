using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace AutoWorkshop.Models
{
    [Table("Employees")]
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? Position { get; set; }

        [MaxLength(20)]
        public string? Phone { get; set; }

        [MaxLength(50)]
        public string? Email { get; set; }

        public int? DepartmentId { get; set; }

        // Навигационные свойства
        public virtual Department? Department { get; set; }
        public virtual User? UserAccount { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
    }
}