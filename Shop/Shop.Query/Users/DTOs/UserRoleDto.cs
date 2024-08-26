using Common.Query;

namespace Shop.Query.Users.DTOs;

public class UserRoleDto : BaseDto
{
    public string RoleTitle { get; set; }
    public long RoleId { get; set; }
}