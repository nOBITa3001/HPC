using HPC.Domain.Common;
using HPC.Domain.Entities;

namespace HPC.Domain.Events
{
    public class ShipCreatedEvent : DomainEvent
    {
        public Ship Ship { get; }

        public ShipCreatedEvent(Ship ship) => Ship = ship;
    }
}
