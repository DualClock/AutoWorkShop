using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoWorkshop.Models
{
    [Table("Receipts")]
    public class Receipt
    {
        [Key]
        public int Id { get; set; }

        public int OrderId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.Now;

        public bool IsPaid { get; set; }

        [MaxLength(50)]
        public string? ReceiptNumber { get; set; }


        public virtual Order? Order { get; set; }
    }
}