namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;

public class UpdateCartResponse 
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int? BranchId { get; set; }

    public DateTime Date { get; set; }

    public decimal Price { get; set; }

    public decimal TotalPrice { get; set; }

    public virtual List<UpdateCartProductPresentation> Products { get; set; } = new();
}