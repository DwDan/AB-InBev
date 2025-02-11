namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;

public class GetCartResult
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateTime Date { get; set; }

    public decimal Price { get; set; }

    public decimal TotalPrice { get; set; }

    public bool IsFinished { get; set; }

    public bool IsCancelled { get; set; }

    public int? BranchId { get; set; }

    public virtual List<GetCartProductApplication> Products { get; set; } = new();
}