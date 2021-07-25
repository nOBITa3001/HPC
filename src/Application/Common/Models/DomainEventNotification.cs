using HPC.Domain.Common;
using MediatR;

namespace HPC.Application.Common.Models
{
    public class DomainEventNotification<TDomainEvent> : INotification where TDomainEvent : DomainEvent
    {
        public TDomainEvent DomainEvent { get; }

        public DomainEventNotification(TDomainEvent domainEvent) => DomainEvent = domainEvent;
    }
}
