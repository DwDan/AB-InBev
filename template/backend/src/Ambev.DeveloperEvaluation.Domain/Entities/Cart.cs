﻿using System.ComponentModel.DataAnnotations;
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

        [ForeignKey("Branch")]
        public int? BranchId { get; set; }

        public virtual Branch? Branch { get; set; }

        public bool Inactive { get; set; }

        public bool IsCancelled { get; set; }

        public bool IsFinished { get; set; }

        public decimal Price { get; set; }

        public decimal TotalPrice { get; set; }

        public virtual ICollection<CartProduct> Products { get; set; } = new List<CartProduct>();

        public virtual User? User { get; set; }
    }
}
