using HPC.Application.Common.Models;
using HPC.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace HPC.Application.TodoItems.EventHandlers
{
    public class RegisterNewShipEventHandler : INotificationHandler<DomainEventNotification<ShipCreatedEvent>>
    {
        private readonly ILogger<RegisterNewShipEventHandler> _logger;

        public RegisterNewShipEventHandler(ILogger<RegisterNewShipEventHandler> logger) => _logger = logger;

        public Task Handle(DomainEventNotification<ShipCreatedEvent> notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("HPC Domain Event - {DomainEvent}", notification.DomainEvent.GetType().Name);
            _logger.LogInformation("HPC registers a new ship - Name: {ShipName} Code: {ShipCode}", notification.DomainEvent.Ship.Name, notification.DomainEvent.Ship.Code);

            return Task.CompletedTask;
        }
    }
}
