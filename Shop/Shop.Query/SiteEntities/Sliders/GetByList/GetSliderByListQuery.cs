using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.SiteEntities.DTOs;
using Shop.Query.SiteEntities.Mappers;

namespace Shop.Query.SiteEntities.Sliders.GetByList;

public class GetSliderByListQuery : IQuery<List<SliderDto>>
{
    
}

public class GetSliderByListQueryHandler : IQueryHandler<GetSliderByListQuery, List<SliderDto>>
{
    private readonly ShopContext _shopContext;

    public GetSliderByListQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }
    public async Task<List<SliderDto>> Handle(GetSliderByListQuery request, CancellationToken cancellationToken)
    {
        var sliders = await _shopContext.Sliders
            .Select(slider => slider.MapToDto()).ToListAsync(cancellationToken);

        return sliders;
    }
}