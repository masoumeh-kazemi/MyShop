using Common.Query;
using Shop.Domain.UserAgg.Enum;

namespace Shop.Query.Users.DTOs;

public class UserDto : BaseDto
{
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string? Name { get; set; }
    public string? Family { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
    public Gender Gender { get; set; }
    public string AvatarImage { get; set; }
    public List<UserRoleDto> UserRoles { get; set; }
    public List<string> RoleTitls { get; set; }
}