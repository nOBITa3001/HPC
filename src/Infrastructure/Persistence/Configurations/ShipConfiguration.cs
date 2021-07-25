using HPC.Domain.Constants;
using HPC.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HPC.Infrastructure.Persistence.Configurations
{
    public class ShipConfiguration : AuditableConfiguration<Ship>
    {
        protected override void ConfigureChild(EntityTypeBuilder<Ship> builder)
        {
            builder.Property(property => property.Name)
                .IsRequired()
                .HasMaxLength(MaxLengthConfiguration.EntityName);

            builder.Property(property => property.Code)
                .IsRequired()
                .HasMaxLength(MaxLengthConfiguration.ShipCode);

            builder.Property(property => property.LengthInMetres)
                .IsRequired();

            builder.Property(property => property.WidthInMetres)
                .IsRequired();

            builder.Ignore(property => property.DomainEvents);
        }
    }
}