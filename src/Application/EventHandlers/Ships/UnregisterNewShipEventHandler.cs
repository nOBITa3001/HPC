using HPC.Application.Common.Models;
using HPC.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace HPC.Application.TodoItems.EventHandlers
{
    public class UnregisterNewShipEventHandler : INotificationHandler<DomainEventNotification<ShipDeletedEvent>>
    {
        private readonly ILogger<UnregisterNewShipEventHandler> _logger;

        public UnregisterNewShipEventHandler(ILogger<UnregisterNewShipEventHandler> logger) => _logger = logger;

        public Task Handle(DomainEventNotification<ShipDeletedEvent> notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("HPC Domain Event - {DomainEvent}", notification.DomainEvent.GetType().Name);
            _logger.LogInformation("HPC unregisters a ship - Name: {ShipName} Code: {ShipCode}", notification.DomainEvent.Ship.Name, notification.DomainEvent.Ship.Code);

            return Task.CompletedTask;
        }
    }
}
