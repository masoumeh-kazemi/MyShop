using System.Data;
using Microsoft.Data.SqlClient;
using Shop.Domain.OrderAgg;

namespace Shop.Infrastructure.Persistent.Dapper;

public class DapperContext
{
    private readonly string _connectionString;

    public DapperContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }


    public string UserTokens => "[user].Tokens";
    public string UserAddresses => "[user].Addresses";
    public string ProductImages => "[product].Images";
    public string SellerInventories => "[seller].Inventories";
    public string Sellers => "[seller].Sellers";

    public string Products => "[product].Products";
    public string Orders => "[order].Orders";
    public string OrderItems => "[order].Items";




}