using Common.Application;
using Common.Application.SecurityUtil;
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Services;

namespace Shop.Application.Users.Register;

public class UserRegisterCommand : IBaseCommand
{
    public string PhoneNumber { get; private set; }
    public string Password { get; private set; }

    public UserRegisterCommand(string phoneNumber, string password)
    {
        PhoneNumber = phoneNumber;
        Password = password;
    }
}

public class UserRegisterCommandHandler : IBaseCommandHandler<UserRegisterCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserDomainService _userDomainService;

    public UserRegisterCommandHandler(IUserRepository userRepository, IUserDomainService userDomainService)
    {
        _userRepository = userRepository;
        _userDomainService = userDomainService;
    }
    public async Task<OperationResult> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
    {
        var hashPassword = Sha256Hasher.Hash(request.Password);
        var userRegistered = User.Register(request.PhoneNumber, hashPassword, _userDomainService);
        _userRepository.Add(userRegistered);
        await _userRepository.Save();
        return OperationResult.Success();
    }
}


