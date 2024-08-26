using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Products.Create;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Products.DTOs;

namespace Shop.Query.Products.GetById;

public record GetProductByIdQuery(long Id) : IQuery<ProductDto>;

public class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, ProductDto>
{
    private readonly ShopContext _shopContext;

    public GetProductByIdQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }
    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _shopContext.Products
            .Include(f=>f.Images)
            .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken: cancellationToken);

        if (product == null)
        {
            return null;
        }

        var result = product.MapToDto();
        return result;
    }
}
