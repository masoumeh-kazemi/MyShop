using Shop.Domain.SiteEntities;
using Shop.Infrastructure._Utilities;

namespace Shop.Infrastructure.Persistent.EF.SiteEntities.ShippingMethods;

public class ShippingMethodRepository : BaseRepository<ShippingMethod>, IShippingMethodRepository
{
    public ShippingMethodRepository(ShopContext context) : base(context)
    {
    }
}