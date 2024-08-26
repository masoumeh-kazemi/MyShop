using Shop.RazorPage.ViewModels;

namespace Shop.RazorPage.Pages.ViewModel;

public class EditCategoryViewModel
{
    public string Slug { get; set; }
    public string Title { get; set; }
    public SeoDataViewModel SeoData { get; set; }
}