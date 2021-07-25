using HPC.Domain.Common;
using System.Collections.Generic;

namespace HPC.Domain.Entities
{
    public class Ship : AuditableEntity, IHasDomainEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal LengthInMetres { get; set; }
        public decimal WidthInMetres { get; set; }

        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}