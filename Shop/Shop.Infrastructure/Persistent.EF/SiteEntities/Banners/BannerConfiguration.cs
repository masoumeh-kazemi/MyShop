using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.SiteEntities;

namespace Shop.Infrastructure.Persistent.EF.SiteEntities.Banners;

public class BannerConfiguration :IEntityTypeConfiguration<Banner>
{
    public void Configure(EntityTypeBuilder<Banner> builder)
    {
        builder.Property(b => b.Link)
            .HasMaxLength(500).IsRequired();
    }
}