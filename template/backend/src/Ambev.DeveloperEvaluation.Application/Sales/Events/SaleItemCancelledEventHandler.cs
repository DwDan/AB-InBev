using Ambev.DeveloperEvaluation.Domain.Events.Sale;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Events.Sales
{
    public class SaleItemCancelledEventHandler : INotificationHandler<SaleItemCancelledEvent>
    {
        private readonly ILogger<SaleItemCancelledEventHandler> _logger;

        public SaleItemCancelledEventHandler(ILogger<SaleItemCancelledEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(SaleItemCancelledEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"SaleItemCancelledEvent handled: SaleId={notification.SaleId}, ItemId={notification.SaleItemId}, Timestamp={notification.Timestamp}");

            return Task.CompletedTask;
        }
    }
}
