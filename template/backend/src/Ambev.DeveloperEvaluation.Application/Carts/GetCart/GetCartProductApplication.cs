using Ambev.DeveloperEvaluation.Application.Products.Common;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;

public class GetCartProductApplication
{
    public int Id { get; set; }

    public int CartId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal UnityPrice { get; set; }

    public decimal Discount { get; set; }

    public decimal TotalPrice { get; set; }

    public virtual ProductApplication? Product { get; set; }
}
