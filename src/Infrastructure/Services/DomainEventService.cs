using HPC.Application.Common.Interfaces;
using HPC.Application.Common.Models;
using HPC.Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace HPC.Infrastructure.Services
{
    public class DomainEventService : IDomainEventService
    {
        private readonly ILogger<DomainEventService> _logger;
        private readonly IPublisher _mediator;

        public DomainEventService(ILogger<DomainEventService> logger, IPublisher mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task Publish(DomainEvent domainEvent)
        {
            static INotification GetNotificationCorrespondingToDomainEvent(DomainEvent domainEvent) =>
                (INotification)Activator.CreateInstance(typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType()), domainEvent);

            _logger.LogInformation("Publishing domain event - {event}", domainEvent.GetType().Name);

            await _mediator.Publish(GetNotificationCorrespondingToDomainEvent(domainEvent));
        }
    }
}