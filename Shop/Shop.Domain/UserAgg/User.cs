using Common.Domain;
using Shop.Domain.UserAgg.Enum;
using Shop.Domain.UserAgg.Services;

namespace Shop.Domain.UserAgg;

public class User : AggregateRoot
{
    private User()
    {

    }

    public User(string name, string family, string phoneNumber, string email, string password
        , Gender gender, IUserDomainService userDomainService)
    {
        Name = name;
        Family = family;
        PhoneNumber = phoneNumber;
        Email = email;
        Password = password;
        AvatarName = "avatar.png";
        IsActive = true;
        Gender = gender;
        Addresses = new List<UserAddress>();
        Tokens = new List<UserToken>();
        Roles = new List<UserRole>();

    }

    public string Name { get; private set; }
    public string Family { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string AvatarName { get; private set; }
    public bool IsActive { get; private set; } = false;
    public Gender Gender { get; private set; }

    public List<UserAddress> Addresses { get; private set; }
    public List<UserRole> Roles { get; private set; }
    public List<UserToken> Tokens { get; private set; }


    public static User Register(string phoneNumber, string password, IUserDomainService domainService)
    {
        return new User("", "", phoneNumber, "", password, Gender.نامشخص, domainService);
    }
}

