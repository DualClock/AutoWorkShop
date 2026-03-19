using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoWorkshop.Models
{
    [Table("OrderParts")]
    public class OrderPart
    {
        [Key]
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int PartId { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PriceAtMoment { get; set; }

        // Навигационные свойства
        public virtual Order? Order { get; set; }
        public virtual Part? Part { get; set; }
    }
}