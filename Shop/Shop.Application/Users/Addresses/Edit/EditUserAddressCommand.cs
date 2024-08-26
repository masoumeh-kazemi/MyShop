using Common.Application;
using Common.Domain.ValueObjects;
using Shop.Domain.UserAgg;

namespace Shop.Application.Users.Addresses.Edit;

public class EditUserAddressCommand : IBaseCommand
{
    public long UserId { get; internal set; }
    public long AddressId { get; private set; }
    public string Shire { get; private set; }
    public string City { get; private set; }
    public string PostalCode { get; private set; }
    public string PostalAddress { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Name { get; private set; }
    public string Family { get; private set; }
    public string NationalCode { get; private set; }

    public EditUserAddressCommand(long userId, long addressId, string shire, string city, string postalCode, string postalAddress, string phoneNumber, string name, string family, string nationalCode)
    {
        UserId = userId;
        AddressId = addressId;
        Shire = shire;
        City = city;
        PostalCode = postalCode;
        PostalAddress = postalAddress;
        PhoneNumber = phoneNumber;
        Name = name;
        Family = family;
        NationalCode = nationalCode;
    }
}


public class EditUserAddressCommandHandler : IBaseCommandHandler<EditUserAddressCommand>
{
    private readonly IUserRepository _userRepository;

    public EditUserAddressCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<OperationResult> Handle(EditUserAddressCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetTracking(request.UserId);
        if (user == null)
        {
            return OperationResult.NotFound();
        }

        user.EditAddress(request.AddressId, request.Shire, request.City, request.PostalCode, request.PostalAddress, 
            request.PhoneNumber, request.Name, request.Family, request.NationalCode);

        await _userRepository.Save();
        return OperationResult.Success();
    }
}

