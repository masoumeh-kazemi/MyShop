using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Roles.DTOs;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Roles.GetById;

public record GetRoleByIdQuery(long Id) : IQuery<RoleDto>;


public class GetRoleByIdQueryHandler : IQueryHandler<GetRoleByIdQuery, RoleDto>
{
    private readonly ShopContext _shopContext;

    public GetRoleByIdQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }
    public async Task<RoleDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _shopContext.Roles
            .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);
        return new RoleDto()
        {
            Title = role.Title,
            Id = role.Id,
            CreationDate = role.CreationDate,
            RolePermissions = role.Permissions.Select(f=>f.Permission).ToList()
        };
    }
}