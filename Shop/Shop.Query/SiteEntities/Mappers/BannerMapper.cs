using Shop.Domain.SiteEntities;
using Shop.Query.SiteEntities.DTOs;

namespace Shop.Query.SiteEntities.Mappers;

public static class BannerMapper
{
    public static BannerDto MapToDto(this Banner banner)
    {
        return new BannerDto()
        {
            CreationDate = banner.CreationDate,
            Id = banner.Id,
            Link = banner.Link,
            ImageName = banner.ImageName,
            Position = banner.Position,
        };
    }
}