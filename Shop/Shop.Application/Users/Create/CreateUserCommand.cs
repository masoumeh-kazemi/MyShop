using Common.Application;
using Common.Application._Utilities;
using Common.Application.FileUtil.Interfaces;
using Common.Application.SecurityUtil;
using Microsoft.AspNetCore.Http;
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Enum;
using Shop.Domain.UserAgg.Services;

namespace Shop.Application.Users.Create;

public class CreateUserCommand : IBaseCommand
{
    public string Name { get; private set; }
    public string Family { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public IFormFile? AvatarImage { get; private set; }
    public bool IsActive { get; private set; } = false;
    public Gender Gender { get; private set; }
    public List<long> RoleIds { get; private set; }

    public CreateUserCommand(string name, string family, string phoneNumber, string email, string password, IFormFile? avatarImage, bool isActive, Gender gender, List<long> roleIds)
    {
        Name = name;
        Family = family;
        PhoneNumber = phoneNumber;
        Email = email;
        Password = password;
        AvatarImage = avatarImage;
        IsActive = isActive;
        Gender = gender;
        RoleIds = roleIds;
    }
}


public class CreateUserCommandHandler : IBaseCommandHandler<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserDomainService _userDomainService;
    private readonly IFileService _fileService;

    public CreateUserCommandHandler(IUserRepository userRepository, IUserDomainService userDomainService, IFileService fileService)
    {
        _userRepository = userRepository;
        _userDomainService = userDomainService;
        _fileService = fileService;
    }
    public async Task<OperationResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var hashPassword = Sha256Hasher.Hash(request.Password);
        var user = new User(request.Name, request.Family, request.PhoneNumber,
            request.Email, hashPassword, request.Gender, _userDomainService);

        if (request.AvatarImage != null)
        {
            var imageName = await _fileService.SaveFileAndGenerateName(request.AvatarImage, Directories.UserAvatars);
            user.SetAvatarName(imageName);
        }

        var userRoles = new List<UserRole>();
        foreach (var item in request.RoleIds)
        {
            userRoles.Add(new UserRole(item));
        }
        user.SetRole(userRoles);

        _userRepository.Add(user);
        await _userRepository.Save();
        return OperationResult.Success();


    }
}