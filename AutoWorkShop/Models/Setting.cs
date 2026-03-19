using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoWorkshop.Models
{
    [Table("Settings")]
    public class Setting
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string KeyName { get; set; } = string.Empty;

        [MaxLength(4000)]
        public string? Value { get; set; }
    }
}