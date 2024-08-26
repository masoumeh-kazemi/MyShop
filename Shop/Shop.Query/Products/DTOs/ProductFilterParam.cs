using Common.Query;
using Common.Query.Filter;

namespace Shop.Query.Products.DTOs;

public class ProductFilterParam : BaseFilterParam
{
    public string Title { get; set; }
    public string Slug { get; set; }
    public long? Id { get; set; } = null;
}