using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Roles.DTOs;

namespace Shop.Query.Roles.GetList;

public class GetRoleByListQuery : IQuery<List<RoleDto>>
{
    

}



public class GetRoleByListQueryHandler : IQueryHandler<GetRoleByListQuery, List<RoleDto>>
{

    private readonly ShopContext _shopContext;

    public GetRoleByListQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<List<RoleDto>> Handle(GetRoleByListQuery request, CancellationToken cancellationToken)
    {
        var roles = await _shopContext.Roles
                .Include(f=>f.Permissions)
                .OrderByDescending(f => f.Id)
                .Select(role => new RoleDto()
                {
                    CreationDate = role.CreationDate,
                    Id = role.Id,
                    Title = role.Title,
                    RolePermissions = role.Permissions.Select(rolePermission => rolePermission.Permission).ToList(),

                }).ToListAsync(cancellationToken)
            ;
        return roles;
    }
}