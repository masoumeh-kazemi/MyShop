using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Products.DTOs;

namespace Shop.Query.Products.GetByList;

public class GetProductByListQuery : IQuery<List<ProductDto>>
{
    
}

public class GetProductByListQueryHandler : IQueryHandler<GetProductByListQuery, List<ProductDto>>
{
    private readonly ShopContext _shopContext;

    public GetProductByListQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }
    public async Task<List<ProductDto>> Handle(GetProductByListQuery request, CancellationToken cancellationToken)
    {
        var products = await _shopContext.Products.Select(product=> product.MapToDto()).ToListAsync(cancellationToken);

        return products;
    }
}