using Common.Application;
using Common.Query;
using Dapper;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.Addresses.GetById;

public class GetUserAddressByIdQuery : IQuery<UserAddressDto>
{
    public long Id { get; private set; }

    public GetUserAddressByIdQuery(long id)
    {
        Id = id;
    }
}

public class GetUserAddressByIdQueryHandler : IQueryHandler<GetUserAddressByIdQuery, UserAddressDto>
{
    private readonly DapperContext _dapperContext;
    public GetUserAddressByIdQueryHandler(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<UserAddressDto> Handle(GetUserAddressByIdQuery request, CancellationToken cancellationToken)
    {
        var connection = _dapperContext.CreateConnection();
        var sql = @$"SELECT * FROM {_dapperContext.UserAddresses} WHERE Id =@id";
        var result = await connection.QueryFirstOrDefaultAsync<UserAddressDto>(sql, new { id = request.Id });
        return result;
    }
}