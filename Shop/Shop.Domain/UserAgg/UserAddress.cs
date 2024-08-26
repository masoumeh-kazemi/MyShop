using Common.Domain;
using Common.Domain.ValueObjects;

namespace Shop.Domain.UserAgg;

public class UserAddress :  BaseEntity
{
    private UserAddress()
    {

    }
    public UserAddress(string shire, string city, string postalCode, string postalAddress, PhoneNumber phoneNumber, string name,
        string family, string nationalCode)
    {
        //Guard(shire, city, postalCode, postalAddress, phoneNumber, name,
        //    family, nationalCode);
        Shire = shire;
        City = city;
        PostalCode = postalCode;
        PostalAddress = postalAddress;
        PhoneNumber = phoneNumber;
        Name = name;
        Family = family;
        NationalCode = nationalCode;
    }
    public long UserId { get; internal set; }
    public string Shire { get; private set; }
    public string City { get; private set; }
    public string PostalCode { get; private set; }
    public string PostalAddress { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public string Name { get; private set; }
    public string Family { get; private set; }
    public string NationalCode { get; private set; }
    public bool ActiveAddress { get; private set; } = false;



    public void SetActive()
    {
        ActiveAddress = true;

    }
    public void SetDeActive()
    {
        ActiveAddress = false;
    }


    public void Edit(string shire, string city, string postalCode, string postalAddress, string phoneNumber, string name,
        string family, string nationalCode)
    {
        Shire = shire;
        City = city;
        PostalCode = postalCode;
        PostalAddress = postalAddress;
        PhoneNumber = new PhoneNumber(phoneNumber);
        Name = name;
        Family = family;
        NationalCode = nationalCode;

    }
}