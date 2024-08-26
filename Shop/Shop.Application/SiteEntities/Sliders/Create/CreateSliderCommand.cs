using Common.Application;
using Common.Application._Utilities;
using Common.Application.FileUtil.Interfaces;
using Microsoft.AspNetCore.Http;
using Shop.Domain.SiteEntities;

namespace Shop.Application.SiteEntities.Sliders.Create;

public class CreateSliderCommand : IBaseCommand
{
    public string Title { get; private set; }
    public string Link { get; private set; }
    public IFormFile ImageFile { get; private set; }

    public CreateSliderCommand(string title, string link, IFormFile imageFile)
    {
        Title = title;
        Link = link;
        ImageFile = imageFile;
    }
}

public class CreateSliderCommandHandler : IBaseCommandHandler<CreateSliderCommand>
{
    private readonly ISliderRepository _sliderRepository;
    private readonly IFileService _fileService;

    public CreateSliderCommandHandler(ISliderRepository sliderRepository, IFileService fileService)
    {
        _sliderRepository = sliderRepository;
        _fileService = fileService;
    }
    public async Task<OperationResult> Handle(CreateSliderCommand request, CancellationToken cancellationToken)
    {
        var imageName = await _fileService.SaveFileAndGenerateName(request.ImageFile, Directories.SliderImages);
        var slider = new Slider(request.Title, request.Link, imageName);

        _sliderRepository.Add(slider);
        await _sliderRepository.Save();
        return OperationResult.Success();
    }
}