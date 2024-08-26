using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.CategoryAgg;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Categories.DTOs;

namespace Shop.Query.Categories.GetById;

public class GetCategoryByIdQuery : IQuery<CategoryDto>
{
    public long Id { get; private set; }

    public GetCategoryByIdQuery(long id)
    {
        Id = id;
    }
}

public class GetCategoryByIdQueryHandler : IQueryHandler<GetCategoryByIdQuery, CategoryDto>
{
    private readonly ShopContext _context;

    public GetCategoryByIdQueryHandler(ShopContext context)
    {
        _context = context;
    }
    public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .Include(f => f.Childs)
            .ThenInclude(f => f.Childs)
            .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken: cancellationToken);

        if (category == null)
        {
            return null;
        }
        return category.MapToDto();
    }
}