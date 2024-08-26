using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.SiteEntities.DTOs;
using Shop.Query.SiteEntities.Mappers;

namespace Shop.Query.SiteEntities.Sliders.GetById;

public class GetSliderByIdQuery : IQuery<SliderDto>
{
    public long Id { get; private set; }

    public GetSliderByIdQuery(long id)
    {
        Id = id;
    }
}

public class GetSliderByIdQueryHandler : IQueryHandler<GetSliderByIdQuery, SliderDto>
{
    private readonly ShopContext _shopContext;

    public GetSliderByIdQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }
    public async Task<SliderDto> Handle(GetSliderByIdQuery request, CancellationToken cancellationToken)
    {
        var slider = await _shopContext.Sliders
            .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);
        if (slider == null)
        {
            return null;
        }
        return slider.MapToDto();
    }
}