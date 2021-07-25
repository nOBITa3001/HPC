using HPC.Application.Common.Interfaces;
using HPC.Domain.Common;
using HPC.Domain.Entities;
using HPC.Domain.Enums;
using HPC.Infrastructure.Identity;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace HPC.Infrastructure.Persistence
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        private readonly IDomainEventService _domainEventService;

        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions,
            ICurrentUserService currentUserService,
            IDomainEventService domainEventService,
            IDateTime dateTime) : base(options, operationalStoreOptions)
        {
            _currentUserService = currentUserService;
            _domainEventService = domainEventService;
            _dateTime = dateTime;
        }

        public DbSet<Ship> Ships { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        MarkHousekeepingInsertion(entry.Entity);
                        break;

                    case EntityState.Modified:
                        MarkHousekeepingUpdation(entry.Entity);
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            await DispatchEvents();

            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        private void MarkHousekeepingInsertion<TEntity>(TEntity entity)
            where TEntity : AuditableEntity
        {
            entity.RecordStatus = RecordStatus.Active;
            entity.CreatedBy = entity.ModifiedBy = _currentUserService.UserId;
            entity.CreatedInUtc = entity.ModifiedInUtc = _dateTime.UtcNow;
        }

        private void MarkHousekeepingUpdation<TEntity>(TEntity entity)
            where TEntity : AuditableEntity
        {
            entity.ModifiedBy = _currentUserService.UserId;
            entity.ModifiedInUtc = _dateTime.UtcNow;
        }

        private async Task DispatchEvents()
        {
            while (true)
            {
                var domainEventEntity = ChangeTracker.Entries<IHasDomainEvent>()
                    .Select(domain => domain.Entity.DomainEvents)
                    .SelectMany(domainEvent => domainEvent)
                    .Where(domainEvent => !domainEvent.IsPublished)
                    .FirstOrDefault();

                if (domainEventEntity is null)
                    break;

                domainEventEntity.IsPublished = true;

                await _domainEventService.Publish(domainEventEntity);
            }
        }
    }
}
