using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Categories.DTOs;

namespace Shop.Query.Categories.GetByParentId;

public class GetChildCategoryByParentIdQuery : IQuery<List<ChildCategoryDto>?>
{
    public GetChildCategoryByParentIdQuery(long parentId)
    {
        ParentId = parentId;
    }

    public long ParentId { get; private set; }
}

public  class GetChildCategoryByParentIdQueryHandler : IQueryHandler<GetChildCategoryByParentIdQuery, List<ChildCategoryDto>?>
{
    private readonly ShopContext _shopContext;

    public GetChildCategoryByParentIdQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }
    public async Task<List<ChildCategoryDto>?> Handle(GetChildCategoryByParentIdQuery request, CancellationToken cancellationToken)
    {
        var childCategories = await _shopContext.Categories
            .Include(f=>f.Childs)
            .Where(f => f.ParentId == request.ParentId).ToListAsync(cancellationToken);

        return childCategories.MapToChildCategoryDtos();
    }
}