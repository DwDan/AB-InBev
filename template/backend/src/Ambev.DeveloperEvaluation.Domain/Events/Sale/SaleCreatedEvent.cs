namespace Ambev.DeveloperEvaluation.Domain.Events.Sale;

/// <summary>
/// Event triggered when a sale is created.
/// </summary>
public class SaleCreatedEvent : SaleEvent
{
    public SaleCreatedEvent(int saleId) : base(saleId) { }
}