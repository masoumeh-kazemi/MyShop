using Common.Query;

namespace Shop.Query.SiteEntities.DTOs;

public class ShippingMethodDto : BaseDto
{
    public int Cost { get; set; }
    public string Title { get; set; }
}