using HPC.Domain.Common;
using HPC.Domain.Constants;
using HPC.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HPC.Infrastructure.Persistence.Configurations
{
    public abstract class AuditableConfiguration<TAuditableEntity> : IEntityTypeConfiguration<TAuditableEntity>
        where TAuditableEntity : AuditableEntity
    {
        public void Configure(EntityTypeBuilder<TAuditableEntity> builder)
        {
            ConfigureChild(builder);

            builder.Property(x => x.RecordStatus)
                .HasDefaultValue(RecordStatus.Active);

            builder.Property(x => x.CreatedBy)
                .IsRequired()
                .HasMaxLength(MaxLengthConfiguration.ExecutedBy);

            builder.Property(x => x.CreatedInUtc)
                .HasDefaultValueSql("GETUTCDATE()")
                .HasColumnType("datetime");

            builder.Property(x => x.ModifiedBy)
                .IsRequired()
                .HasMaxLength(MaxLengthConfiguration.ExecutedBy);

            builder.Property(x => x.ModifiedInUtc)
                .HasDefaultValueSql("GETUTCDATE()")
                .HasColumnType("datetime");
        }

        protected abstract void ConfigureChild(EntityTypeBuilder<TAuditableEntity> builder);
    }
}
