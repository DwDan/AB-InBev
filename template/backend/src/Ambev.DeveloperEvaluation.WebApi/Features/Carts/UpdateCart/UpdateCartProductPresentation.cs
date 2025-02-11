using Ambev.DeveloperEvaluation.WebApi.Features.Carts.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.Common;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;

public class UpdateCartProductPresentation : CartProductPresentation
{
    public decimal UnityPrice { get; set; }

    public decimal Discount { get; set; }

    public decimal TotalPrice { get; set; }

    public ProductPresentation? Product { get; set; }
}
