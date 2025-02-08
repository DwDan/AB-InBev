namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.Common
{
    public class CartProductPresentation
    {
        public int Id { get; set; }

        public int CartId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
