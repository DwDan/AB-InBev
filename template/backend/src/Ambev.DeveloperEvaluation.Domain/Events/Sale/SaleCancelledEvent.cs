namespace Ambev.DeveloperEvaluation.Domain.Events.Sale;

/// <summary>
/// Event triggered when a sale is cancelled.
/// </summary>
public class SaleCancelledEvent : SaleEvent
{
    public SaleCancelledEvent(int saleId) : base(saleId) { }
}