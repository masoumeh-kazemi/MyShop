using Shop.Domain.SiteEntities;
using Shop.Query.SiteEntities.DTOs;

namespace Shop.Query.SiteEntities.Mappers;

public static class SliderMapper
{
    public static SliderDto MapToDto(this Slider slider)
    {
        return new SliderDto()
        {
            CreationDate = slider.CreationDate,
            Id = slider.Id,
            ImageName = slider.ImageName,
            Link = slider.Link,
            Title = slider.Title,

        };
    }
}