using Common.Application;
using Common.Application.SecurityUtil;
using Shop.Domain.UserAgg;

namespace Shop.Application.Users.ChangePassword;

public class ChangeUserPasswordCommand : IBaseCommand
{
    public long UserId { get; private set; }
    public string CurrentPassword { get; private set; }
    public string NewPassword { get; private set; }

    public ChangeUserPasswordCommand(long userId, string currentPassword, string newPassword)
    {
        UserId = userId;
        CurrentPassword = currentPassword;
        NewPassword = newPassword;
    }
}

public class ChangeUserPasswordCommandHandler : IBaseCommandHandler<ChangeUserPasswordCommand>
{
    private readonly IUserRepository _userRepository;

    public ChangeUserPasswordCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<OperationResult> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetTracking(request.UserId);
        if (user == null)
            return OperationResult.NotFound();

        var currentPasswordHash = Sha256Hasher.Hash(request.CurrentPassword);
        if (user.Password != currentPasswordHash)
            return OperationResult.Error("کلمه عبور فعلی نامعتبر است");

        var hashNewPassword = Sha256Hasher.Hash(request.NewPassword);
        user.ChangePassword(hashNewPassword);
        await _userRepository.Save();

        return OperationResult.Success();
    }

}