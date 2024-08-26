using Common.Query;
using Shop.Domain.RoleAgg.Enum;

namespace Shop.Query.Roles.DTOs;

public class RoleDto : BaseDto
{
    public string Title { get; set; }
    public List<Permission> RolePermissions { get; set; }

}

public class RolePermissionDto
{
    public Permission Permission { get; set; }
}