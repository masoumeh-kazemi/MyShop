using Common.Application;
using Shop.Domain.UserAgg;

namespace Shop.Application.Users.Addresses.SetActiveAddress;

public class SetUserActiveAddressCommand : IBaseCommand
{
    public long UserId { get; private set; }
    public long AddressId { get; private set; }

    public SetUserActiveAddressCommand(long userId, long addressId)
    {
        UserId = userId;
        AddressId = addressId;
    }
}


public class SetUserActiveAddressCommandHandler : IBaseCommandHandler<SetUserActiveAddressCommand>
{
    private readonly IUserRepository _userRepository;

    public SetUserActiveAddressCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public  async Task<OperationResult> Handle(SetUserActiveAddressCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetTracking(request.UserId);
        if (user == null)
        {
            return OperationResult.NotFound();
        }

        user.SetActiveAddress(request.AddressId);
        await _userRepository.Save();
        return OperationResult.Success();
    }
}