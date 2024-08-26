using Common.Query;
using Shop.Domain.SiteEntities.Enum;

namespace Shop.Query.SiteEntities.DTOs;

public class BannerDto : BaseDto
{
    public string Link { get; set; }
    public BannerPosition Position { get; set; }
    public string ImageName { get; set; }
}