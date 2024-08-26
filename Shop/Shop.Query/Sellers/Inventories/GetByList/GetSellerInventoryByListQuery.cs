using Common.Query;
using Dapper;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Query.Sellers.Inventories.DTOs;

namespace Shop.Query.Sellers.Inventories.GetByList;

public class GetSellerInventoryByListQuery : IQuery<List<InventoryDto>>
{
    public long SellerId { get; private set; }

    public GetSellerInventoryByListQuery(long sellerId)
    {
        SellerId = sellerId;
    }
}

public class GetSellerInventoryByListQueryHandler : IQueryHandler<GetSellerInventoryByListQuery, List<InventoryDto>>
{
    private readonly DapperContext _dapperContext;

    public GetSellerInventoryByListQueryHandler(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }
    public async Task<List<InventoryDto>> Handle(GetSellerInventoryByListQuery request, CancellationToken cancellationToken)
    {
        var connection = _dapperContext.CreateConnection();
        var sql = $@"SELECT Count, Price, DiscountPercentage,i.Id, 
                  p.ImageName AS ProductImageName, p.Title AS ProductTitle 
                  FROM {_dapperContext.SellerInventories} As i
                  INNER JOIN {_dapperContext.Sellers} as s on i.SellerId = s.Id 
                  INNER JOIN {_dapperContext.Products} As p
                  ON i.ProductId = P.Id 
                  WHERE i.SellerId = @sellerId";

        var result = await connection
            .QueryAsync<InventoryDto>(sql, new {sellerId = request.SellerId});
        return result.ToList();
    }
}