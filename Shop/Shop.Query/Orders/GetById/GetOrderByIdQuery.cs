using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Orders.DTOs;

namespace Shop.Query.Orders.GetById;

public class GetOrderByIdQuery : IQuery<OrderDto>
{
    public long Id { get; private set; }

    public GetOrderByIdQuery(long id)
    {
        Id = id;
    }
}

public class GetOrderByIdQueryHandler : IQueryHandler<GetOrderByIdQuery, OrderDto>
{
    private readonly ShopContext _shopContext;
    private readonly DapperContext _dapperContext;
    public GetOrderByIdQueryHandler(ShopContext shopContext, DapperContext dapperContext)
    {
        _shopContext = shopContext;
        _dapperContext = dapperContext;
    }
    public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _shopContext.Orders
            .Include(f=>f.ShippingMethod)
            .Include(f=>f.Address)
            .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken: cancellationToken);

        if (order == null)
        {
            return null;
        }

        var result = order.MapToDto();
        await result.GetOrderItem(_dapperContext);
        return result;
    }
}