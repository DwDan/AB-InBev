namespace Ambev.DeveloperEvaluation.Domain.Events.Sale;

/// <summary>
/// Event triggered when an item is cancelled within a sale.
/// </summary>
public class SaleItemCancelledEvent : SaleEvent
{
    public int SaleItemId { get; }

    public SaleItemCancelledEvent(int saleId, int saleItemId) : base(saleId)
    {
        SaleItemId = saleItemId;
    }
}
