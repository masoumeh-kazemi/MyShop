using Common.Application;
using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.GetById;

public record GetUserByIdQuery(long Id) : IQuery<UserDto>;

public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserDto>
{
    private readonly ShopContext _shopContext;

    public GetUserByIdQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }
    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _shopContext.Users
            .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken: cancellationToken);
        if (user == null)
        {
            return null;
        }

        return user.MapToDto().SetRoleTitle(_shopContext);
    }
}