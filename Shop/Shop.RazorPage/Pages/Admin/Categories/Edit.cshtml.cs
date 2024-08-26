using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Categories.Edit;
using Shop.Presentation.Facade.Categories;
using Shop.Presentation.Facade.Users;
using Shop.RazorPage.Pages.Infrastructure.RazorUtil;
using Shop.RazorPage.Pages.ViewModel;
using Shop.RazorPage.ViewModels;

namespace Shop.RazorPage.Pages.Admin.Categories
{
    [BindProperties]
    public class EditModel : BaseRazorPage
    {        
        [Display(Name = "Slug")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Slug { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Title { get; set; }


        public SeoDataViewModel SeoData { get; set; }

        private readonly ICategoryFacade _categoryFacade;

        public EditModel(ICategoryFacade categoryFacade)
        {
            _categoryFacade = categoryFacade;
        }
        public async Task OnGet(long id)
        {
            var category = await _categoryFacade.GetById(id);
            if (category == null)
            {
                return;
            }

            SeoData = SeoDataViewModel.MapSeoDataToViewModel(category.SeoData);
            Title = category.Title;
            Slug = category.Slug;
        }

        public async Task<IActionResult> OnPost(long id,EditCategoryViewModel editViewModel)
        {
            var result = await _categoryFacade.Edit(new EditCategoryCommand(id, editViewModel.Title,
                editViewModel.Slug, SeoDataViewModel.MapViewModelToSeoData(editViewModel.SeoData)));

            return RedirectAndShowAlert(result, RedirectToPage("Index"));
        }
    }
}
