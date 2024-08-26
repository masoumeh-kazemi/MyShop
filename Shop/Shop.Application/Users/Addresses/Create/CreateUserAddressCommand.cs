using Common.Application;
using Common.Domain.ValueObjects;
using Shop.Domain.UserAgg;

namespace Shop.Application.Users.Addresses.Create;

public class CreateUserAddressCommand : IBaseCommand
{
    public long UserId { get; internal set; }
    public string Shire { get; private set; }
    public string City { get; private set; }
    public string PostalCode { get; private set; }
    public string PostalAddress { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Name { get; private set; }
    public string Family { get; private set; }
    public string NationalCode { get; private set; }

    public CreateUserAddressCommand(long userId, string shire, string city, string postalCode, string postalAddress, string phoneNumber, string name, string family, string nationalCode)
    {
        UserId = userId;
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


public class CreateUserAddressCommandHandler : IBaseCommandHandler<CreateUserAddressCommand>
{
    private readonly IUserRepository _userRepository;

    public CreateUserAddressCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<OperationResult> Handle(CreateUserAddressCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetTracking(request.UserId);
        if (user == null)
        {
            return OperationResult.NotFound();
        }

        user.AddAddress(new UserAddress(request.Shire, request.City, request.PostalCode,
            request.PostalAddress,new PhoneNumber(request.PhoneNumber), request.Name, request.Family, request.NationalCode));
        
        await _userRepository.Save();
        return OperationResult.Success();
    }
}