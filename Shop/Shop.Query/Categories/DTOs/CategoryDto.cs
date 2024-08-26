using Common.Domain.ValueObjects;
using Common.Query;

namespace Shop.Query.Categories.DTOs;

public class CategoryDto :  BaseDto
{
    public string Title { get; set; }
    public string Slug { get; set; }
    public SeoData SeoData { get; set; }
    public List<ChildCategoryDto> ChildCategory { get; set; }
}

public class ChildCategoryDto :  BaseDto
{
    public string Title { get; set; }
    public string Slug { get; set; }
    public SeoData SeoData { get; set; }
    public long ParentId { get; set; }

    public List<SecondaryChildCategoryDto> SecondaryChild { get; set; }
}

public class SecondaryChildCategoryDto : BaseDto
{
    public string Title { get; set; }
    public string Slug { get; set; }
    public SeoData SeoData { get; set; }
    public long ParentId { get; set; }

}