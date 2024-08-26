using Common.Query;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Products.DTOs;

namespace Shop.Query.Products.GetByFilter;

public class GetProductByFilterQuery : QueryFilter<ProductFilterResult, ProductFilterParam>
{
    public GetProductByFilterQuery(ProductFilterParam filterParams) : base(filterParams)
    {
    }
}

public class GetProductByFilterQueryHandler : IQueryHandler<GetProductByFilterQuery, ProductFilterResult>
{
    private readonly ShopContext _shopContext;

    public GetProductByFilterQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }
    public async Task<ProductFilterResult> Handle(GetProductByFilterQuery request, CancellationToken cancellationToken)
    {
        var @params = request.FilterParams;
        var result = _shopContext.Products
            .OrderByDescending(f => f.Id)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(@params.Title))
            result = result.Where(f => f.Title.Contains(@params.Title));

        if (!string.IsNullOrWhiteSpace(@params.Slug))
            result = result.Where(f => f.Title.Contains(@params.Slug));

        if (@params.Id != null)
            result = result.Where(f => f.Id == @params.Id);


        var skip = (@params.PageId - 1) * @params.Take;

        var model = new ProductFilterResult()
        {
            FilterParams = @params,
            Data = result.Skip(skip).Take(@params.Take).Select(product => product.MapToDto()).ToList(),
        };
        model.GeneratePaging(result, @params.Take, @params.PageId);
        return model;
    }
}