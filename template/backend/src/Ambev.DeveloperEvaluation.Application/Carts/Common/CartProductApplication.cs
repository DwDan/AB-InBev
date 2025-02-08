namespace Ambev.DeveloperEvaluation.Application.Carts.Common
{
    public class CartProductApplication
    {
        public int Id { get; set; }

        public int CartId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
