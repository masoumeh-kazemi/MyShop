using Common.Query;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.SiteEntities.DTOs;
using Shop.Query.SiteEntities.Mappers;

namespace Shop.Query.SiteEntities.Banners.GetById;

public record GetBannerByIdQuery(long Id) : IQuery<BannerDto>;

public  class GetBannerByIdQueryHandler : IQueryHandler<GetBannerByIdQuery, BannerDto>
{
    private readonly ShopContext _shopContext;

    public GetBannerByIdQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }
    public async Task<BannerDto> Handle(GetBannerByIdQuery request, CancellationToken cancellationToken)
    {
        var banner = _shopContext.Banners.FirstOrDefault(f => f.Id == request.Id);
        if (banner == null)
        {
            return null;
        }

        return banner.MapToDto();
    }
}