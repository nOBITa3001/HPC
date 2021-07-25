using HPC.Domain.Common;
using System.Threading.Tasks;

namespace HPC.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
