using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace AutoWorkshop.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int CarId { get; set; }

        public int EmployeeId { get; set; }

        [MaxLength(4000)]
        public string? Description { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "New";

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalCost { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? CompletedDate { get; set; }

        

       
        public virtual Car? Car { get; set; }
        public virtual Employee? Employee { get; set; }
        public virtual ICollection<OrderPart>? OrderParts { get; set; }
        public virtual ICollection<Receipt>? Receipts { get; set; }
    }
}