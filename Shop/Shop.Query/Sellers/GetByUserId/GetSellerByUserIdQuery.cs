using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.SellerAgg;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Sellers.Inventories;
using Shop.Query.Sellers.Inventories.DTOs;

namespace Shop.Query.Sellers.GetByUserId;

public class GetSellerByUserIdQuery :  IQuery<SellerDto>
{
    public GetSellerByUserIdQuery(long userId)
    {
        UserId = userId;
    }

    public long UserId { get; private set; }
}

public class GetSellerByUserIdQueryHandler : IQueryHandler<GetSellerByUserIdQuery, SellerDto>
{
    private readonly ShopContext _shopContext;

    public GetSellerByUserIdQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }
    public async Task<SellerDto> Handle(GetSellerByUserIdQuery request, CancellationToken cancellationToken)
    {
        var seller = await _shopContext.Sellers
            .FirstOrDefaultAsync(f => f.UserId == request.UserId && f.Status==SellerStatus.Accepted, cancellationToken);

        if (seller == null)
        {
            return null;
        }

        return seller.MapToDto();
    }
}