using Common.Domain.Exceptions;
using Common.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.SiteEntities;

namespace Shop.Infrastructure.Persistent.EF.SiteEntities.ShippingMethods;

public class ShippingMethodConfiguration : IEntityTypeConfiguration<ShippingMethod>
{
    public void Configure(EntityTypeBuilder<ShippingMethod> builder)
    {
        builder.Property(b => b.Title)
            .HasMaxLength(120).IsRequired();
    }
}