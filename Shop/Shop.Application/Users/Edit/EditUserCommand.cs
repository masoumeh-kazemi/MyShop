using Common.Application;
using Common.Application._Utilities;
using Common.Application.FileUtil.Interfaces;
using Common.Application.SecurityUtil;
using Microsoft.AspNetCore.Http;
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Enum;
using Shop.Domain.UserAgg.Services;

namespace Shop.Application.Users.Edit;

public class EditUserCommand : IBaseCommand
{
    public long Id { get; private set; }
    public string Name { get; private set; }
    public string Family { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public IFormFile? AvatarImage { get; private set; }
    public bool IsActive { get; private set; } = false;
    public Gender Gender { get; private set; }
    public List<long> RoleIds { get; private set; }

    public EditUserCommand(long id, string name, string family, string phoneNumber, string email, IFormFile? avatarImage, bool isActive, Gender gender, List<long> roleIds)
    {
        Id = id;
        Name = name;
        Family = family;
        PhoneNumber = phoneNumber;
        Email = email;
        AvatarImage = avatarImage;
        IsActive = isActive;
        Gender = gender;
        RoleIds = roleIds;
    }
}

public class EditUserCommandHandler : IBaseCommandHandler<EditUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserDomainService _userDomainService;
    private readonly IFileService _fileService;

    public EditUserCommandHandler(IUserRepository userRepository, IUserDomainService userDomainService, IFileService fileService)
    {
        _userRepository = userRepository;
        _userDomainService = userDomainService;
        _fileService = fileService;
    }
    public async Task<OperationResult> Handle(EditUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetTracking(request.Id);
        if (user == null)
        {
            return OperationResult.NotFound();
        }

        if (request.AvatarImage != null)
        {
            _fileService.DeleteFile(Directories.GetUserAvatar(user.AvatarName));
            var newAvatarName = await _fileService.SaveFileAndGenerateName(request.AvatarImage, Directories.UserAvatars);
            user.SetAvatarName(newAvatarName);
        }

        user.Edit(request.Name, request.Family, request.PhoneNumber, request.Email, request.Gender, _userDomainService);

        var userRoles = new List<UserRole>();
        foreach (var item in request.RoleIds)
        {
            userRoles.Add(new UserRole(item));
        }
        user.SetRole(userRoles);
        await _userRepository.Save();
        return OperationResult.Success();
    }
}