using System.ComponentModel.DataAnnotations;
using Common.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Categories.AddChild;
using Shop.Application.Categories.Create;
using Shop.Presentation.Facade.Categories;
using Shop.RazorPage.Pages.Infrastructure.RazorUtil;
using Shop.RazorPage.ViewModels;

namespace Shop.RazorPage.Pages.Admin.Categories
{
    [BindProperties]
    public class AddModel : BaseRazorPage
    {        
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Title { get; set; }

        [Display(Name = "Slug")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Slug { get; set; }

        public SeoDataViewModel SeoData { get; set; }
        private readonly ICategoryFacade _categoryFacade;

        public AddModel(ICategoryFacade categoryFacade)
        {
            _categoryFacade = categoryFacade;
        }
        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPost(long? parentId)
        {
            var seoData = SeoDataViewModel.MapViewModelToSeoData(SeoData);

            if (parentId != null)
            {
                var child = await _categoryFacade
                    .AddChild(new AddCategoryChildCommand((long)parentId, Title, Slug, seoData));

                return RedirectAndShowAlert(child, RedirectToPage("Index"));

            }

            var result = await _categoryFacade
                .Create(new CreateCategoryCommand(Title, Slug, seoData));

            return RedirectAndShowAlert(result, RedirectToPage("Index"));

        }
    }
}
