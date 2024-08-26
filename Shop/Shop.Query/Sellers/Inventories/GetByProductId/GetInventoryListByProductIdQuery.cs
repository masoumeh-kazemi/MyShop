using Common.Query;
using Dapper;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Query.Sellers.Inventories.DTOs;

namespace Shop.Query.Sellers.Inventories.GetByProductId;

public class GetInventoryListByProductIdQuery : IQuery<List<InventoryDto>?>
{
    public long ProductId { get; private set; }

    public GetInventoryListByProductIdQuery(long productId)
    {
        ProductId = productId;
    }
}

public class GetInventoryListByProductIdQueryHandler : IQueryHandler<GetInventoryListByProductIdQuery, List<InventoryDto>?>
{
    private readonly DapperContext _dapperContext;

    public GetInventoryListByProductIdQueryHandler(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }
    public async Task<List<InventoryDto>?> Handle(GetInventoryListByProductIdQuery request, CancellationToken cancellationToken)
    {
        var connection = _dapperContext.CreateConnection();
        var sql = $@"SELECT i.Id, i.SellerId , i.ProductId ,i.Count , i.Price,i.CreationDate , i.DiscountPercentage , s.ShopName ,
                  p.Title as ProductTitle,p.ImageName as ProductImage
                  From {_dapperContext.SellerInventories} i 
                  INNER JOIN {_dapperContext.Sellers} s on i.SellerId=s.Id 
                  INNER JOIN {_dapperContext.Products} p on i.ProductId = p.Id
                  WHERE ProductId=@productId";

        var result = await connection
            .QueryAsync<InventoryDto>(sql, new { productId = request.ProductId });
        return result.ToList();
    }
}