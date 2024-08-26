using Common.Application;
using Common.Domain.ValueObjects;
using Shop.Domain.CategoryAgg;
using Shop.Domain.CategoryAgg.Services;

namespace Shop.Application.Categories.Create;

public class CreateCategoryCommand : IBaseCommand
{
    public string Title { get; private set; }
    public string Slug { get; private set; }
    public SeoData SeoData { get; private set; }

    public CreateCategoryCommand(string title, string slug, SeoData seoData)
    {
        Title = title;
        Slug = slug;
        SeoData = seoData;
    }
}

public class CreateCategoryCommandHandler : IBaseCommandHandler<CreateCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICategoryDomainService _categoryDomainService;
    

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, ICategoryDomainService categoryDomainService)
    {
        _categoryRepository = categoryRepository;
        _categoryDomainService = categoryDomainService;
    }
    public async Task<OperationResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category(request.Title, request.Slug, request.SeoData, _categoryDomainService);
        _categoryRepository.Add(category);
        await _categoryRepository.Save();
        return OperationResult.Success();
    }
}