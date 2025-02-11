namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.Common
{
    public class CartPresentation
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime Date { get; set; }

        public bool IsFinished { get; set; }

        public bool IsCancelled { get; set; }

        public int? BranchId { get; set; }

        public virtual List<CartProductPresentation> Products { get; set; } = new();
    }
}
