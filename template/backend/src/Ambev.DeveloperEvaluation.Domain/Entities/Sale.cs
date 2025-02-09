using System.ComponentModel.DataAnnotations.Schema;
using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale : BaseEntity
    {    
        /// <summary>
         /// Gets or sets the date when the sale was made.
         /// </summary>
        public DateTime Date { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the unique identifier of the customer (user).
        /// </summary>
        [ForeignKey("User")]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the customer who made the purchase.
        /// </summary>
        public virtual User User { get; set; } = new();

        /// <summary>
        /// Gets or sets the unique identifier of the customer (user).
        /// </summary>
        [ForeignKey("Branch")]
        public int BranchId { get; set; }

        /// <summary>
        /// Gets or sets the branch who made the purchase.
        /// </summary>
        public virtual Branch Branch { get; set; } = new();

        /// <summary>
        /// Gets or sets the unique identifier of the cart.
        /// </summary>
        [ForeignKey("Cart")]
        public int CartId { get; set; }

        /// <summary>
        /// Gets or sets the cart who made the purchase.
        /// </summary>
        public virtual Cart Cart { get; set; } = new();

        /// <summary>
        /// Gets or sets the total amount of the sale.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the status of the sale (Cancelled or Not Cancelled).
        /// </summary>
        public bool IsCancelled { get; set; } = false;


        public virtual ICollection<SaleProduct> Products { get; set; } = new List<SaleProduct>();
    }
}
