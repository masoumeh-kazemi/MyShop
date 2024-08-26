using Common.Query;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.OrderAgg;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Orders.DTOs;

namespace Shop.Query.Orders.GetCurrentByUserId;

public class GetCurrentOrderByUserIdQuery : IQuery<OrderDto?>
{
    public long UserId { get; set; }

    public GetCurrentOrderByUserIdQuery(long userId)
    {
        UserId = userId;
    }
}

public class GetCurrentOrderByUserIdQueryHandler : IQueryHandler<GetCurrentOrderByUserIdQuery, OrderDto?>
{
    private readonly ShopContext _shopContext;
    private  readonly DapperContext _dapperContext;

    public GetCurrentOrderByUserIdQueryHandler(DapperContext dapperContext, ShopContext shopContext)
    {
        _dapperContext = dapperContext;
        _shopContext = shopContext;
    }
    public async Task<OrderDto?> Handle(GetCurrentOrderByUserIdQuery request, CancellationToken cancellationToken)
    {
       var order = await _shopContext.Orders.FirstOrDefaultAsync
            (f => f.UserId == request.UserId && f.Status == OrderStatus.Pending, cancellationToken);
        if (order == null)
            return null;

        var result = order.MapToDto();
        await result.GetOrderItem(_dapperContext);
        return result;
    }


}

