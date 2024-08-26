using Common.Application;
using Common.Domain.ValueObjects;
using Shop.Domain.CategoryAgg;
using Shop.Domain.CategoryAgg.Services;
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Services;

namespace Shop.Application.Categories.AddChild;

public class AddCategoryChildCommand : IBaseCommand
{
    public long ParentId { get; private set; }
    public string Title { get; private set; }
    public string Slug { get; private set; }
    public SeoData SeoData { get; private set; }

    public AddCategoryChildCommand(long parentId, string title, string slug, SeoData seoData)
    {
        ParentId = parentId;
        Title = title;
        Slug = slug;
        SeoData = seoData;
    }
}

public class AddCategoryChildCommandHandler : IBaseCommandHandler<AddCategoryChildCommand>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICategoryDomainService _categoryDomainService;


    public AddCategoryChildCommandHandler(ICategoryRepository categoryRepository, ICategoryDomainService categoryDomainService)
    {
        _categoryRepository = categoryRepository;
        _categoryDomainService = categoryDomainService;
    }

    public async Task<OperationResult> Handle(AddCategoryChildCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetTracking(request.ParentId);
        if (category == null)
        {
            return OperationResult.NotFound();
        }

        category.AddChild(request.ParentId, request.Title, request.Slug, request.SeoData, _categoryDomainService);
        await _categoryRepository.Save();
        return OperationResult.Success();
    }
}