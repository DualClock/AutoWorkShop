using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace AutoWorkshop.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Login { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Role { get; set; } = string.Empty;

        public int? EmployeeId { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime? LastLogin { get; set; }


        public virtual Employee? Employee { get; set; }
        public virtual ICollection<Notification>? Notifications { get; set; }
    }
}