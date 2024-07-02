using Shop.Domain.UserAgg;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users;

public static class UserMapper
{
    public static UserDto MapToDto(this User user)
    {
        var result = new UserDto()
            {
                PhoneNumber = user.PhoneNumber,
                Password = user.Password,
                Name = user.Name,
                Family = user.Family,
            };

        return result;
    }
}