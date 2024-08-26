using Common.Application;
using Shop.Domain.UserAgg;

namespace Shop.Application.Users.Addresses.Delete;

public class DeleteUserAddressCommand : IBaseCommand
{
    public long UserId { get; private set; }
    public long AddressId { get; private set; }

    public DeleteUserAddressCommand(long userId, long addressId)
    {
        UserId = userId;
        AddressId = addressId;
    }
}

public class DeleteUserAddressCommandHandler : IBaseCommandHandler<DeleteUserAddressCommand>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserAddressCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<OperationResult> Handle(DeleteUserAddressCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetTracking(request.UserId);
        if (user == null)
        {
            return OperationResult.NotFound();
        }
        user.DeleteAddress(request.AddressId);

        await _userRepository.Save();
        return OperationResult.Success();
    }
}