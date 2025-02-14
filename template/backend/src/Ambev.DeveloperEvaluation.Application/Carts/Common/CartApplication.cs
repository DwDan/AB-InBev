﻿namespace Ambev.DeveloperEvaluation.Application.Carts.Common
{
    public class CartApplication
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime Date { get; set; }

        public bool IsFinished { get; set; }

        public bool IsCancelled { get; set; }

        public int? BranchId { get; set; }

        public virtual List<CartProductApplication> Products { get; set; } = new();
    }
}
