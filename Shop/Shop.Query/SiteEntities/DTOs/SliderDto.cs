using System.Security.AccessControl;
using Common.Query;

namespace Shop.Query.SiteEntities.DTOs;

public class SliderDto : BaseDto
{
    public string  Title { get; set; }
    public string ImageName { get; set; }
    public string Link { get; set; }

}