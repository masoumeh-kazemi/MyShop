using Common.Application;
using Common.Application._Utilities;
using Common.Application.FileUtil.Interfaces;
using Microsoft.AspNetCore.Http;
using Shop.Domain.SiteEntities;

namespace Shop.Application.SiteEntities.Sliders.Edit;

public class EditSliderCommand : IBaseCommand
{
    public long Id { get; private set; }
    public string Title { get; private set; }
    public string Link { get; private set; }
    public IFormFile? ImageFile { get; private set; }

    public EditSliderCommand(long id, string title, string link, IFormFile? imageFile)
    {
        Id = id;
        Title = title;
        Link = link;
        ImageFile = imageFile;
    }
}

public class EditSliderCommandHandler : IBaseCommandHandler<EditSliderCommand>
{
    private readonly ISliderRepository _sliderRepository;
    private readonly IFileService _fileService;

    public EditSliderCommandHandler(ISliderRepository sliderRepository, IFileService fileService)
    {
        _sliderRepository = sliderRepository;
        _fileService = fileService;
    }
    public async Task<OperationResult> Handle(EditSliderCommand request, CancellationToken cancellationToken)
    {
        var slider = await _sliderRepository.GetTracking(request.Id);
        if (slider == null)
            return OperationResult.NotFound();

        slider.Edit(request.Title, request.Link);
        if (request.ImageFile != null)
        {
            var newImageName = await _fileService.SaveFileAndGenerateName(request.ImageFile, Directories.SliderImages);
            var oldImageName = slider.ImageName;
            _fileService.DeleteFile(Directories.SliderImages, oldImageName);
            slider.SetImageName(newImageName);
        }

        await _sliderRepository.Save();
        return OperationResult.Success();
    }
}