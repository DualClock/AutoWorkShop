using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace AutoWorkshop.Models
{
    [Table("Departments")]
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        // Навигационное свойство
        public virtual ICollection<Employee>? Employees { get; set; }
    }
}