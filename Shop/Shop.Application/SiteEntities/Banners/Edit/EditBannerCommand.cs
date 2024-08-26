using Common.Application;
using Common.Application._Utilities;
using Common.Application.FileUtil.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Domain.SiteEntities;
using Shop.Domain.SiteEntities.Enum;

namespace Shop.Application.SiteEntities.Banners.Edit;

public class EditBannerCommand : IBaseCommand
{
    public long Id { get; private set; }
    public string Link { get; private set; }
    public IFormFile? ImageFile { get; private set; }
    public BannerPosition Position { get; private set; }

    public EditBannerCommand(long id, string link, IFormFile? imageFile, BannerPosition position)
    {
        Id = id;
        Link = link;
        ImageFile = imageFile;
        Position = position;
    }
}

public class EditBannerCommandHandler : IBaseCommandHandler<EditBannerCommand>
{
    private readonly IBannerRepository _bannerRepository;
    private readonly IFileService _fileService;

    public EditBannerCommandHandler(IBannerRepository bannerRepository, IFileService fileService)
    {
        _bannerRepository = bannerRepository;
        _fileService = fileService;
    }
    public async Task<OperationResult> Handle(EditBannerCommand request, CancellationToken cancellationToken)
    {
        var banner = await _bannerRepository.GetTracking(request.Id);
        if (banner == null)
        {
            return OperationResult.NotFound();
        }

        banner.Edit(request.Link, request.Position);

        if (request.ImageFile != null)
        {
            var newImageName = await _fileService.SaveFileAndGenerateName(request.ImageFile, Directories.BannerImages);
            var oldImageName = banner.ImageName;
            _fileService.DeleteFile(Directories.BannerImages, oldImageName);
            banner.SetImageName(newImageName);
        }
        await _bannerRepository.Save();
        return OperationResult.Success();

    }
}