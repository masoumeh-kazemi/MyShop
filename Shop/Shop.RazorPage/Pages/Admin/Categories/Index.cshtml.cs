using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Presentation.Facade.Categories;
using Shop.Query.Categories.DTOs;

namespace Shop.RazorPage.Pages.Admin.Categories
{
    public class IndexModel : PageModel
    {
        public List<CategoryDto> Categories { get; set; }
        private readonly ICategoryFacade _categoryFacade;

        public IndexModel(ICategoryFacade categoryFacade)
        {
            _categoryFacade = categoryFacade;
        }
        public async Task OnGet()
        {
            var categories = await _categoryFacade.GetByList();
            Categories = categories;
        }
    }
}
