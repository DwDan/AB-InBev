using Ambev.DeveloperEvaluation.Application.Carts.Common;
using Ambev.DeveloperEvaluation.Application.Products.Common;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

public class UpdateCartResult
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateTime Date { get; set; }

    public virtual List<UpdateCartProductApplication> Products { get; set; } = new();
}

public class UpdateCartProductApplication : CartProductApplication
{
    public ProductApplication? Product { get; set; }
}
