using Common.Query.Filter;
using Microsoft.AspNetCore.Mvc;

namespace Shop.RazorPage.Pages.Infrastructure.RazorUtil;

public class BaseRazorFilter<TFilterParam> : BaseRazorPage where TFilterParam : BaseFilterParam, new()
{
    [BindProperty(SupportsGet = true)]
    public TFilterParam FilterParam { get; set; }

    public BaseRazorFilter()
    {
        FilterParam = new TFilterParam();
    }
}