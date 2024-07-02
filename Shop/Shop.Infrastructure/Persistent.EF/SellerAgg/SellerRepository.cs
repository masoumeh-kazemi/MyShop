using Shop.Domain.SellerAgg;
using Shop.Infrastructure._Utilities;

namespace Shop.Infrastructure.Persistent.EF.SellerAgg;

public class SellerRepository : BaseRepository<Seller>,ISellerRepository
{
    public SellerRepository(ShopContext context) : base(context)
    {
    }
}