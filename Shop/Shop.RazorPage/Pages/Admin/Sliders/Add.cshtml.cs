using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.SiteEntities.Sliders.Create;
using Shop.Presentation.Facade.SiteEntities.Sliders;
using Shop.RazorPage.Pages.Infrastructure.RazorUtil;

namespace Shop.RazorPage.Pages.Admin.Sliders
{
    [BindProperties]
    public class AddModel : BaseRazorPage
    {      
        [Display(Name = "لینک")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Link { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Title { get; set; }

        [Display(Name = "تصویر")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public IFormFile ImageFile { get; set; }

        private readonly ISliderFacade _sliderFacade;

        public AddModel(ISliderFacade sliderFacade)
        {
            _sliderFacade = sliderFacade;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var result = await _sliderFacade
                .Create(new CreateSliderCommand(Title, Link, ImageFile));

            return RedirectAndShowAlert(result, RedirectToPage("Index"));
        }
    }
}
