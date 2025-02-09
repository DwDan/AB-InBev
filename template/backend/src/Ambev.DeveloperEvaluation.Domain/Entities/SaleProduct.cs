using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleProduct : BaseEntity
    {
        [ForeignKey("Sale")]
        public int SaleId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public virtual Sale Sale { get; set; } = new();

        public virtual Product Product { get; set; } = new();

        public decimal Discount { get; set; }
    }
}
