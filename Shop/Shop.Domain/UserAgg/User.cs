using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;
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
        Roles = new List<UserRole>(){new UserRole(1)};

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

    public void Edit(string name, string family, string phoneNumber, string email
        , Gender gender, IUserDomainService userDomainService)
    {
        Name = name;
        Family = family;
        PhoneNumber = phoneNumber;
        Email = email;
        Gender = gender;
    }

    public void EditProfile(string name, string family, string phoneNumber, string email
        , Gender gender, IUserDomainService userDomainService)
    {
        Name = name;
        Family = family;
        PhoneNumber = phoneNumber;
        Email = email;
        Gender = gender;
    }

    public void SetAvatarName(string avatarName)
    {
        AvatarName = avatarName;
    }

    public void SetRole(List<UserRole> roles)
    {
        Roles = roles;
    }

    public void AddToken(UserToken token)
    {
        token.UserId = Id;
        Tokens.Add(token);
    }


    public void DeleteToken(long tokenId)
    {
        var token = Tokens.FirstOrDefault(f => f.Id == tokenId);
        if (token==null)
        {
            throw new NullOrEmptyDomainDataException();
        }

        Tokens.Remove(token);
    }

    public void ChangePassword(string password)
    {
        Password = password;
    }


    public void AddAddress(UserAddress address)
    {
        address.UserId = Id;
        Addresses.Add(address);
        SetActiveAddress(address.Id);
    }


    public void EditAddress(long addressId, string shire, string city, string postalCode, string postalAddress, string phoneNumber, string name,
        string family, string nationalCode)
    {
        var address = Addresses.FirstOrDefault(f => f.Id == addressId);
        if (address == null)
        {
            throw new NullOrEmptyDomainDataException();
        }

        address.UserId = Id; 
        address.Edit(shire, city, postalCode, postalAddress, phoneNumber, name, family, nationalCode);
    }

    public void DeleteAddress(long addressId)
    {
        var address = Addresses.FirstOrDefault(f=>f.Id == addressId);
        if (address == null)
        {
            throw new NullOrEmptyDomainDataException();
        }

        Addresses.Remove(address);
    }
    public void SetActiveAddress(long addressId)
    {
        var activeAddress = Addresses.FirstOrDefault(f => f.Id == addressId);
        if (activeAddress == null)
            throw new NullOrEmptyDomainDataException();

        foreach (var item in Addresses)
        {
            item.SetDeActive();
        }
        activeAddress.SetActive();
    }



}

