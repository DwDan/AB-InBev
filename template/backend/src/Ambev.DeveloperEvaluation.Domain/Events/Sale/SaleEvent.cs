using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events.Sale;

/// <summary>
/// Base event for sales domain.
/// </summary>
public abstract class SaleEvent : INotification
{
    public int SaleId { get; }
    public DateTime Timestamp { get; } = DateTime.UtcNow;

    protected SaleEvent(int saleId)
    {
        SaleId = saleId;
    }
}
