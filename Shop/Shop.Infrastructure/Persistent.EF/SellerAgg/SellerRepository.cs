using Dapper;
using Shop.Domain.SellerAgg;
using Shop.Infrastructure._Utilities;
using Shop.Infrastructure.Persistent.Dapper;

namespace Shop.Infrastructure.Persistent.EF.SellerAgg;

public class SellerRepository : BaseRepository<Seller>,ISellerRepository
{
    private readonly DapperContext _dapperContext;

    public SellerRepository(ShopContext context, DapperContext dapperContext) : base(context)
    {
        _dapperContext = dapperContext;
    }


    public async Task<InventoryResult?> GetInventoryById(long id)
    {
        var connection = _dapperContext.CreateConnection();
        var sql = $@"SELECT i.ProductId, i.SellerId, i.Price, i.Count, i.DiscountPercentage FROM {_dapperContext.SellerInventories} AS i
        WHERE i.Id=@inventoryId";

        var result = await connection.QueryFirstOrDefaultAsync<InventoryResult>(sql, new { inventoryId = id });
        return result;
    }
}