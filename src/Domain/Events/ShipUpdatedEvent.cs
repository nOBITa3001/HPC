using HPC.Domain.Common;
using HPC.Domain.Entities;

namespace HPC.Domain.Events
{
    public class ShipUpdatedEvent : DomainEvent
    {
        public Ship Ship { get; }

        public ShipUpdatedEvent(Ship ship) => Ship = ship;
    }
}
