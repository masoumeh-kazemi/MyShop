using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Orders.DTOs;

namespace Shop.Query.Orders.GetByFilter;

public class GetOrderByFilterQuery : QueryFilter<OrderFilterResult, OrderFilterParam>
{
    public GetOrderByFilterQuery(OrderFilterParam filterParams) : base(filterParams)
    {
    }
}


public class GetOrderByFilterQueryHandler : IQueryHandler<GetOrderByFilterQuery, OrderFilterResult>
{
    private readonly ShopContext _shopContext;

    public GetOrderByFilterQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }
    public async Task<OrderFilterResult> Handle(GetOrderByFilterQuery request, CancellationToken cancellationToken)
    {
        var @params = request.FilterParams;
        var result = _shopContext.Orders.AsQueryable();

        if (@params.UserId != null)
        {
            result = result.Where(f => f.UserId.Equals(@params.UserId));
        }

        if (@params.Status != null)
            result = result.Where(f => f.Status == @params.Status);


        if (@params.StartDate != null)
            result = result.Where(f => f.CreationDate == @params.StartDate);

        if (@params.EndDate != null)
            result = result.Where(f => f.LastUpdate == @params.EndDate);

        var skip = (@params.PageId - 1) * @params.Take;

        var model = new OrderFilterResult()
        {
            Data = await result.Skip(skip).Take(@params.Take)
                .Select(order => order.MapToFilterData(_shopContext)).ToListAsync(cancellationToken),

            FilterParams = @params
        };
        model.GeneratePaging(result, @params.Take, @params.PageId);
        return model;
    }
}