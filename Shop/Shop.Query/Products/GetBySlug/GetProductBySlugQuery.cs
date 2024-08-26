using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Products.DTOs;

namespace Shop.Query.Products.GetBySlug;

public record GetProductBySlugQuery(string Slug) : IQuery<ProductDto>;

public class GetProductBySlugQueryHandler : IQueryHandler<GetProductBySlugQuery, ProductDto>
{
    private readonly ShopContext _shopContext;

    public GetProductBySlugQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }
    public async Task<ProductDto> Handle(GetProductBySlugQuery request, CancellationToken cancellationToken)
    {
        var product = await _shopContext.Products
            .Include(f=>f.Images)
            .Include(f=>f.Specifications)
            .FirstOrDefaultAsync(f => f.Slug == request.Slug, cancellationToken);
        if (product == null)
            return null;

        var result = product.MapToDto();
        result.SetCategoryProduct(_shopContext);
        return result;

    }
}