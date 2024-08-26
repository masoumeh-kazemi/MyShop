using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.SiteEntities.DTOs;
using Shop.Query.SiteEntities.Mappers;

namespace Shop.Query.SiteEntities.Banners.GetByList;

public class GetBannerByListQuery : IQuery<List<BannerDto>>
{

}

public class GetBannerByListQueryHandler : IQueryHandler<GetBannerByListQuery, List<BannerDto>>
{
    private readonly ShopContext _shopContext;

    public GetBannerByListQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }
    public async Task<List<BannerDto>> Handle(GetBannerByListQuery request, CancellationToken cancellationToken)

    {
        var banners = await _shopContext.Banners.Select(banner => banner.MapToDto()).ToListAsync(cancellationToken);
        return banners;
    }
}