using Common.Domain;
using Common.Domain.Utils;
using Common.Domain.ValueObjects;
using Shop.Domain.CategoryAgg.Services;

namespace Shop.Domain.CategoryAgg;

public class Category : AggregateRoot
{
    private Category()
    {
        Childs = new List<Category>();
    }
    public Category(string title, string slug, SeoData seoData, ICategoryDomainService service)
    {
        slug = slug?.ToSlug();
        //Guard(title, slug, service);
        Title = title;
        Slug = slug;
        SeoData = seoData;
        Childs = new List<Category>();
    }

    public string Title { get; private set; }
    public string Slug { get; private set; }
    public SeoData SeoData { get; private set; }
    public long? ParentId { get; private set; }
    public List<Category> Childs { get; private set; }
}