using Common.Query;
using Dapper;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Query.Sellers.Inventories.DTOs;

namespace Shop.Query.Sellers.Inventories.GetById;

public class GetSellerInventoryById : IQuery<InventoryDto?>
{
    public long InventoryId { get; private set; }

    public GetSellerInventoryById(long inventoryId)
    {
        InventoryId = inventoryId;
    }
}


public class GetSellerInventoryByIdHandler : IQueryHandler<GetSellerInventoryById, InventoryDto?>
{
    private readonly DapperContext _dapperContext;

    public GetSellerInventoryByIdHandler(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }
    public async Task<InventoryDto?> Handle(GetSellerInventoryById request, CancellationToken cancellationToken)
    {

        var connection = _dapperContext.CreateConnection();
        var sql = @$"SELECT Top(1) i.Id, i.SellerId , ProductId ,Count , Price,i.CreationDate , DiscountPercentage , s.ShopName,
                       p.Title as ProductTitle,p.ImageName as ProductImageName
                  FROM {_dapperContext.SellerInventories} i inner join {_dapperContext.Sellers} s on i.SellerId=s.Id  
                  inner join {_dapperContext.Products} p on i.ProductId=p.Id WHERE i.Id=@id";

        var result = await connection.QueryFirstOrDefaultAsync<InventoryDto>(sql, new { id = @request.InventoryId });
        return result;

    }
}