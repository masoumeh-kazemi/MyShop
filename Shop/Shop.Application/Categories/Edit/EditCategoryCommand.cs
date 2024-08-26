using Common.Application;
using Common.Domain.ValueObjects;
using Shop.Domain.CategoryAgg;

namespace Shop.Application.Categories.Edit;

public class EditCategoryCommand : IBaseCommand
{
    public long Id { get; private set; }
    public string Title { get; private set; }
    public string Slug { get; private set; }
    public SeoData SeoData { get; private set; }

    public EditCategoryCommand(long id, string title, string slug, SeoData seoData)
    {
        Id = id;
        Title = title;
        Slug = slug;
        SeoData = seoData;
    }
}


public class EditCategoryCommandHandler : IBaseCommandHandler<EditCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;

    public EditCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    public async Task<OperationResult> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetTracking(request.Id);

        if (category == null)
        {
            return OperationResult.NotFound();
        }

        category.Edit(request.Title, request.Slug, request.SeoData);
        await _categoryRepository.Save();
        return OperationResult.Success();
    }
}