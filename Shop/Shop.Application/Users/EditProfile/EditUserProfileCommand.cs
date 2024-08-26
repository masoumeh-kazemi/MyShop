using Common.Application;
using Common.Application._Utilities;
using Common.Application.FileUtil.Interfaces;
using Microsoft.AspNetCore.Http;
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Enum;
using Shop.Domain.UserAgg.Services;

namespace Shop.Application.Users.EditProfile;

public class EditUserProfileCommand : IBaseCommand
{
    public long Id { get; private set; }
    public string Name { get; private set; }
    public string Family { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public IFormFile? AvatarImage { get; private set; }
    public Gender Gender { get; private set; }

    public EditUserProfileCommand(long id, string name, string family, string phoneNumber, string email, IFormFile? avatarImage, Gender gender)
    {
        Id = id;
        Name = name;
        Family = family;
        PhoneNumber = phoneNumber;
        Email = email;
        AvatarImage = avatarImage;
        Gender = gender;
    }
}

public class EditUserProfileCommandHandler : IBaseCommandHandler<EditUserProfileCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserDomainService _userDomainService;
    private readonly IFileService _fileService;

    public EditUserProfileCommandHandler(IUserRepository userRepository, IUserDomainService userDomainService, IFileService fileService)
    {
        _userRepository = userRepository;
        _userDomainService = userDomainService;
        _fileService = fileService;
    }
    public async Task<OperationResult> Handle(EditUserProfileCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetTracking(request.Id);
        if(user == null)
            return OperationResult.NotFound();
        if (request.AvatarImage != null)
        {
            var oldImageName = user.AvatarName;
            _fileService.DeleteFile(Directories.GetUserAvatar(oldImageName));
            var imageName = await _fileService.SaveFileAndGenerateName(request.AvatarImage, Directories.UserAvatars);
           user.SetAvatarName(imageName);
        }

        
        user.EditProfile(request.Name, request.Family, request.PhoneNumber, request.Email, request.Gender,
            _userDomainService);

        await _userRepository.Save();
        return OperationResult.Success();
    }
}