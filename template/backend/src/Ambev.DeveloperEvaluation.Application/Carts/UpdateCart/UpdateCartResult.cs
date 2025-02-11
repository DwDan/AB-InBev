namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

public class UpdateCartResult
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateTime Date { get; set; }

    public decimal Price { get; set; }

    public decimal TotalPrice { get; set; }

    public virtual List<UpdateCartProductApplication> Products { get; set; } = new();
}
