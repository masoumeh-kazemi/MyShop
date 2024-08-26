using Shop.Domain.UserAgg;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users;

public static class UserMapper
{
    public static UserDto MapToDto(this User user)
    {
        var result = new UserDto()
            {
                Id = user.Id,
                PhoneNumber = user.PhoneNumber,
                Password = user.Password,
                Name = user.Name,
                Family = user.Family,
                Email = user.Email,
                IsActive = user.IsActive,
                Gender = user.Gender,
                AvatarImage = user.AvatarName,
                UserRoles = user.Roles.MapToUserRoleDto(),

            };

        return result;
    }

    public static  List<UserRoleDto> MapToUserRoleDto(this List<UserRole> userRoles)
    {
        var result = new List<UserRoleDto>();
        foreach (var item in userRoles)
        {
            result.Add(new UserRoleDto()
            {
                Id = item.Id,
                RoleId = item.RoleId,
                CreationDate = item.CreationDate,
                RoleTitle = "",
            });
        }



        return result;

    }

    public static UserDto SetRoleTitle(this UserDto user, ShopContext context)
    {
        var roleTitles = new List<string>();
        var userRoleIds = user.UserRoles.Select(f => f.RoleId);
        var roles = context.Roles.Where(role => userRoleIds.Contains(role.Id));
        foreach (var role in roles)
        {
            
            roleTitles.Add(role.Title);
        }
         
        user.RoleTitls = roleTitles;
        return user;

    }
}