using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Users;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.GetByPhoneNumber;

public record GetUserByPhoneNumberQuery(string PhoneNumber) : IQuery<UserDto>;


public class GetUserByPhoneNumberQueryHandler : IQueryHandler<GetUserByPhoneNumberQuery, UserDto>
{
    private readonly ShopContext _shopContext;

    public GetUserByPhoneNumberQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }
    public async Task<UserDto> Handle(GetUserByPhoneNumberQuery request, CancellationToken cancellationToken)
    {
        var user = await _shopContext.Users
            .Include(f=>f.Roles)
            .FirstOrDefaultAsync(f => f.PhoneNumber == request.PhoneNumber, cancellationToken);
        if (user == null)
        {
            return null;
        }

        var result = user.MapToDto();
        return result.SetRoleTitle(_shopContext);
    }
}