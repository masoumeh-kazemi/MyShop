using Shop.Domain.CategoryAgg;
using Shop.Query.Categories.DTOs;

namespace Shop.Query.Categories;

public static class CategoryMapper
{
    public static CategoryDto MapToDto(this Category category)
    {
        return new CategoryDto()
        {
            CreationDate = category.CreationDate,
            Id = category.Id,
            Title = category.Title,
            Slug = category.Slug,
            SeoData = category.SeoData,
            ChildCategory = category.Childs.Select(child=> new ChildCategoryDto()
            {
                ParentId = (long)child.ParentId,
                Title = child.Title,
                Slug = child.Slug,
                SeoData = child.SeoData,
                SecondaryChild = child.Childs.Select(secondChild=> new SecondaryChildCategoryDto()
                {
                    ParentId = (long)secondChild.ParentId,
                    Title = secondChild.Title,
                    Slug = secondChild.Slug,
                    SeoData = secondChild.SeoData,

                }).ToList(),

            }).ToList(),
        };
    }

    public static List<ChildCategoryDto> MapToChildCategoryDtos(this List<Category> categories)
    {
        var result = new List<ChildCategoryDto>();
        foreach (var item in categories)
        {
            result.Add(new ChildCategoryDto()
            {
                CreationDate = item.CreationDate,
                Id = item.Id,
                ParentId = (long)item.ParentId,
                Title = item.Title,
                Slug = item.Slug,
                SeoData = item.SeoData,
                SecondaryChild = item.Childs.Select(secondChild=> new SecondaryChildCategoryDto()
                {
                    CreationDate = secondChild.CreationDate,
                    Id = secondChild.Id,
                    ParentId = (long)secondChild.ParentId,
                    SeoData = secondChild.SeoData,
                    Slug = secondChild.Slug,
                    Title = secondChild.Title
                }).ToList(),
            });
        }
        return result;
    }


}