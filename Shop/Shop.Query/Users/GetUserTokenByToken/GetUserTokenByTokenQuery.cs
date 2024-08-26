using Common.Query;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.GetUserTokenByToken;

public class GetUserTokenByTokenQuery : IQuery<UserTokenDto>
{
    public string HashJwtToken { get;private set; }

    public GetUserTokenByTokenQuery(string hashJwtToken)
    {
        HashJwtToken = hashJwtToken;
    }
}

public class GetUserByTokenQueryHandler : IQueryHandler<GetUserTokenByTokenQuery, UserTokenDto>
{
    private readonly DapperContext _dapperContext;

    public GetUserByTokenQueryHandler(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }
    public async Task<UserTokenDto> Handle(GetUserTokenByTokenQuery request, CancellationToken cancellationToken)
    {
        using var connection = _dapperContext.CreateConnection();
        var sql = $"SELECT TOP(1) * FROM {_dapperContext.UserTokens} Where HasJwtToken=@hashJwtToken";
        var result = await connection.QueryFirstOrDefaultAsync<UserTokenDto>(sql, new { hashJwtToken = request.HashJwtToken });
        return result;
    }
}