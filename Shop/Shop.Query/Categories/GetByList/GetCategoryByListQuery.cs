using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Categories.DTOs;

namespace Shop.Query.Categories.GetByList;

public class GetCategoryByListQuery : IQuery<List<CategoryDto>>
{
    
}

public class GetCategoryByListQueryHandler : IQueryHandler<GetCategoryByListQuery, List<CategoryDto>>
{
    private readonly ShopContext _shopContext;

    public GetCategoryByListQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }
    public async Task<List<CategoryDto>> Handle(GetCategoryByListQuery request, CancellationToken cancellationToken)
    {
        var categories = await _shopContext.Categories
            .Where(f=>f.ParentId == null)
            .Include(f => f.Childs)
            .ThenInclude(f => f.Childs)
            .ToListAsync(cancellationToken: cancellationToken);

        var result = categories.Select(category => category.MapToDto()).ToList();
        return result;
    }
}