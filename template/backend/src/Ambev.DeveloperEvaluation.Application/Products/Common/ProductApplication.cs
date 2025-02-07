﻿namespace Ambev.DeveloperEvaluation.Application.Products.Common
{
    public class ProductApplication
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public RatingApplication Rating { get; set; } = new();
    }
}
