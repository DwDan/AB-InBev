using Ambev.DeveloperEvaluation.WebApi.Features.Products.Common;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;

public class GetCartProductPresentation
{
    public int Id { get; set; }

    public int CartId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal UnityPrice { get; set; }

    public decimal Discount { get; set; }

    public decimal TotalPrice { get; set; }

    public ProductPresentation? Product { get; set; }
}
