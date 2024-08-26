using Common.Application;
using Shop.Domain.UserAgg;

namespace Shop.Application.Users.AddToken;

public class AddUserTokenCommand : IBaseCommand
{
    public long UserId { get; private set; }
    public string HasJwtToken { get; private set; }
    public string HashRefreshToken { get; private set; }
    public DateTime TokenExpireDate { get; private set; }
    public DateTime RefreshTokenExpireDate { get; private set; }
    public string Device { get; private set; }

    public AddUserTokenCommand(long userId, string hasJwtToken, string hashRefreshToken, DateTime tokenExpireDate, DateTime refreshTokenExpireDate, string device)
    {
        UserId = userId;
        HasJwtToken = hasJwtToken;
        HashRefreshToken = hashRefreshToken;
        TokenExpireDate = tokenExpireDate;
        RefreshTokenExpireDate = refreshTokenExpireDate;
        Device = device;
    }
}


public class AddUserTokenCommandHandler : IBaseCommandHandler<AddUserTokenCommand>
{
    private readonly IUserRepository _userRepository;

    public AddUserTokenCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<OperationResult> Handle(AddUserTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetTracking(request.UserId);
        var token = new UserToken(request.HasJwtToken, request.HashRefreshToken, request.TokenExpireDate
            , request.RefreshTokenExpireDate, request.Device);
        user.AddToken(token);
        await _userRepository.Save();
        return OperationResult.Success();

    }
}