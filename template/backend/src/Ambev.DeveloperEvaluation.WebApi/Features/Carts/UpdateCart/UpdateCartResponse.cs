using Ambev.DeveloperEvaluation.WebApi.Features.Carts.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.Common;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;

public class UpdateCartResponse 
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateTime Date { get; set; }

    public virtual List<UpdateCartProductPresentation> Products { get; set; } = new();
}


public class UpdateCartProductPresentation : CartProductPresentation
{
    public ProductPresentation? Product { get; set; }
}