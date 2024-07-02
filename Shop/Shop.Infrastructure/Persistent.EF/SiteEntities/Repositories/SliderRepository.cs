using Shop.Domain.SiteEntities;
using Shop.Infrastructure._Utilities;

namespace Shop.Infrastructure.Persistent.EF.SiteEntities.Repositories;

public class SliderRepository : BaseRepository<Slider>,ISliderRepository
{
    public SliderRepository(ShopContext context) : base(context)
    {
    }
}