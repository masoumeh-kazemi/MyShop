using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Sellers.Inventories;
using Shop.Query.Sellers.Inventories.DTOs;

namespace Shop.Query.Sellers.GetById;

public record GetSellerByIdQuery(long Id) : IQuery<SellerDto>;


public class GetSellerByIdQueryHandler : IQueryHandler<GetSellerByIdQuery, SellerDto>
{
    private readonly ShopContext _shopContext;

    public GetSellerByIdQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }
    public async Task<SellerDto> Handle(GetSellerByIdQuery request, CancellationToken cancellationToken)
    {
        var seller = await _shopContext.Sellers
            .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);

        if (seller == null)
        {
            return null;
        }

        return seller.MapToDto();
    }
}