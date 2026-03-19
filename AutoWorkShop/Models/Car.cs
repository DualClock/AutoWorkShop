using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace AutoWorkshop.Models
{
    [Table("Cars")]
    public class Car
    {
        [Key]
        public int Id { get; set; }

        public int ClientId { get; set; }

        [MaxLength(50)]
        public string? Brand { get; set; }

        [MaxLength(50)]
        public string? Model { get; set; }

        [MaxLength(50)]
        public string? VIN { get; set; }

        [MaxLength(20)]
        public string? PlateNumber { get; set; }

        public int? Year { get; set; }


        public virtual Client? Client { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
    }
}