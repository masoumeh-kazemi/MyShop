﻿using Common.Query;
using Shop.Domain.UserAgg.Enum;

namespace Shop.Query.Users.DTOs;

public class UserFilterData : BaseDto
{
    public string Name { get; set; }
    public string Family { get; set; }
    public string PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string AvatarName { get; set; }
    public Gender Gender { get; set; }
}