using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public bool Inactive { get; set; }

        public virtual ICollection<CartProduct> Products { get; set; } = new List<CartProduct>();

        public virtual User? User { get; set; }
    }
}
