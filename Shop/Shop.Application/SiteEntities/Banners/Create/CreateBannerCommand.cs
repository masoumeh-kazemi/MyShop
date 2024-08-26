using Common.Application;
using Common.Application._Utilities;
using Common.Application.FileUtil.Interfaces;
using Microsoft.AspNetCore.Http;
using Shop.Application.SiteEntities.Banners.Create;
using Shop.Domain.SiteEntities;
using Shop.Domain.SiteEntities.Enum;

namespace Shop.Application.SiteEntities.Banners.Create;

public class CreateBannerCommand : IBaseCommand
{
    public string Link { get; private set; }
    public IFormFile ImageFile  { get; private set; }
    public BannerPosition Position { get; private set; }

    public CreateBannerCommand(string link, IFormFile imageFile, BannerPosition position)
    {
        Link = link;
        ImageFile = imageFile;
        Position = position;
    }
}

public class CreateBannerCommandHandler : IBaseCommandHandler<CreateBannerCommand>
{
    private readonly IBannerRepository _bannerRepository;
    private readonly IFileService _fileService;


    public CreateBannerCommandHandler(IBannerRepository bannerRepository, IFileService fileService)
    {
        _bannerRepository = bannerRepository;
        _fileService = fileService;
    }
    public async Task<OperationResult> Handle(CreateBannerCommand request, CancellationToken cancellationToken)
    {
        var imageName = await _fileService.SaveFileAndGenerateName(request.ImageFile, Directories.BannerImages);
        var banner = new Banner(request.Link, imageName, request.Position);
        _bannerRepository.Add(banner);
        await _bannerRepository.Save();
        return OperationResult.Success();
    }
}