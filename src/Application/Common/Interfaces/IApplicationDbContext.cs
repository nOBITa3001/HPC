using HPC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace HPC.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Ship> Ships { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
