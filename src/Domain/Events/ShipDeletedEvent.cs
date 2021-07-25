using HPC.Domain.Common;
using HPC.Domain.Entities;

namespace HPC.Domain.Events
{
    public class ShipDeletedEvent : DomainEvent
    {
        public Ship Ship { get; }

        public ShipDeletedEvent(Ship ship) => Ship = ship;
    }
}
