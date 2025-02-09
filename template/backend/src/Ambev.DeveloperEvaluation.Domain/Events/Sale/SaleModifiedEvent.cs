namespace Ambev.DeveloperEvaluation.Domain.Events.Sale;

/// <summary>
/// Event triggered when a sale is modified.
/// </summary>
public class SaleModifiedEvent : SaleEvent
{
    public SaleModifiedEvent(int saleId) : base(saleId) { }
}