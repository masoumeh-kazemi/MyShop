using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Sellers.Inventories;
using Shop.Query.Sellers.Inventories.DTOs;

namespace Shop.Query.Sellers.GetByFilter;

public class GetSellerByFilterQuery : QueryFilter<SellerFilterResult, SellerFilterParam>
{
    public GetSellerByFilterQuery(SellerFilterParam param) : base(param)
    {
    }
}

public class GetSellerByFilterQueryHandler : IQueryHandler<GetSellerByFilterQuery, SellerFilterResult>
{
    private readonly ShopContext _shopContext;

    public GetSellerByFilterQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }
    public async Task<SellerFilterResult> Handle(GetSellerByFilterQuery request, CancellationToken cancellationToken)
    {
        var @params = request.FilterParams;
        var result = _shopContext.Sellers.AsQueryable();

        if (!string.IsNullOrWhiteSpace(@params.NationalCode))
            result = result.Where(f => f.NationalCode.Contains(@params.NationalCode));

        if (!string.IsNullOrWhiteSpace(@params.ShopName))
            result = result.Where(f => f.ShopName.Contains(@params.ShopName));

        var skip = (@params.PageId - 1) * @params.Take;

        var model = new SellerFilterResult()
        {

            Data = await result.Skip(skip).Take(@params.Take).Select(seller => seller.MapToDto())
                .ToListAsync(cancellationToken),
            FilterParams = @params

        };
        model.GeneratePaging(result, @params.Take, @params.PageId);
        return model;
        
    }
}

