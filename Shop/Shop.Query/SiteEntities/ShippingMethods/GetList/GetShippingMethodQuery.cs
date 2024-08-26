using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.SiteEntities.DTOs;

namespace Shop.Query.SiteEntities.ShippingMethods.GetList;

public class GetShippingMethodQuery : IQuery<List<ShippingMethodDto>>
{
    
}

public class GetShippingMethodQueryHandler : IQueryHandler<GetShippingMethodQuery, List<ShippingMethodDto>>
{
    private readonly ShopContext _shopContext;

    public GetShippingMethodQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<List<ShippingMethodDto>> Handle(GetShippingMethodQuery request, CancellationToken cancellationToken)
    {
        var shippingMethods = await _shopContext.ShippingMethods.Select(shipping => new ShippingMethodDto()
        {
            Title = shipping.Title,
            Cost = shipping.Cost,
            Id = shipping.Id,
        }).ToListAsync(cancellationToken: cancellationToken);
        return shippingMethods;
    }
}