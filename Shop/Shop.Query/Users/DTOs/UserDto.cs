using Common.Query;

namespace Shop.Query.Users.DTOs;

public class UserDto : BaseDto
{
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Family { get; set; }

    public List<UserRoleDto> UserRoles { get; set; }
}


public class UserRoleDto : BaseDto
{
    public string RoleTitle { get; set; }
}