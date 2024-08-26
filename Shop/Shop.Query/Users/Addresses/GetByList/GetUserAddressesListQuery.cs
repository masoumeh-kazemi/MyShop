using Common.Query;
using Dapper;
using Shop.Infrastructure.Persistent.Dapper;

namespace Shop.Query.Users.Addresses.GetByList;

public class GetUserAddressesListQuery : IQuery<List<UserAddressDto>>
{
    public long UserId { get; private set; }

    public GetUserAddressesListQuery(long userId)
    {
        UserId = userId;
    }
}

public class GetUserAddressListQueryHandler : IQueryHandler<GetUserAddressesListQuery, List<UserAddressDto>>
{
    private readonly DapperContext _dapperContext;

    public GetUserAddressListQueryHandler(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }
    public async Task<List<UserAddressDto>> Handle(GetUserAddressesListQuery request, CancellationToken cancellationToken)
    {
        var connection = _dapperContext.CreateConnection();
        var sql = $@"SELECT * From {_dapperContext.UserAddresses} WHERE UserId=@userId";
        var result = await connection.QueryAsync<UserAddressDto>(sql, new {userId=@request.UserId});
        return result.ToList();
    }
}