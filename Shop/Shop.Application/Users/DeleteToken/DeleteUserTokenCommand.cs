using Common.Application;
using Shop.Domain.UserAgg;

namespace Shop.Application.Users.DeleteToken;

public class DeleteUserTokenCommand : IBaseCommand
{
    public long UserId { get; private set; }
    public long TokenId { get; private set; }

    public DeleteUserTokenCommand(long userId, long tokenId)
    {
        UserId = userId;
        TokenId = tokenId;
    }
}


public class DeleteUserTokenCommandHandler : IBaseCommandHandler<DeleteUserTokenCommand>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserTokenCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<OperationResult> Handle(DeleteUserTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetTracking(request.UserId);
        if (user == null)
        {
            return OperationResult.NotFound();
        } 


        user.DeleteToken(request.TokenId);
        await _userRepository.Save();
        return OperationResult.Success();
    }
}