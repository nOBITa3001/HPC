using HPC.Domain.Enums;
using System;

namespace HPC.Domain.Common
{
    public abstract class AuditableEntity
    {
        public RecordStatus RecordStatus { get; set; }
        public DateTime CreatedInUtc { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedInUtc { get; set; }
        public string ModifiedBy { get; set; }
    }
}
