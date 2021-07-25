using System;

namespace HPC.Domain.Common
{
    public abstract class DomainEvent
    {
        public bool IsPublished { get; set; }
        public DateTimeOffset DateOccurredInUtc { get; protected set; } = DateTime.UtcNow;

        protected DomainEvent() => DateOccurredInUtc = DateTimeOffset.UtcNow;
    }
}
