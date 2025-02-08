namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.Common
{
    public class CartPresentation
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime Date { get; set; }

        public virtual List<CartProductPresentation> Products { get; set; } = new();
    }
}
