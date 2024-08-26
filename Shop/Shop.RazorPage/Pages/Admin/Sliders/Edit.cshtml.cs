using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.SiteEntities.Sliders.Edit;
using Shop.Presentation.Facade.SiteEntities.Sliders;
using Shop.RazorPage.Pages.Infrastructure.RazorUtil;

namespace Shop.RazorPage.Pages.Admin.Sliders
{
    [BindProperties]
    public class EditModel : BaseRazorPage
    {
        [Display(Name = "لینک")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Link { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Title { get; set; }

        [Display(Name = "تصویر")]
        public IFormFile? ImageFile { get; set; }

        private readonly ISliderFacade _sliderFacade;

        public EditModel(ISliderFacade sliderFacade)
        {
            _sliderFacade = sliderFacade;
        }
        public async Task OnGet(long id)
        {
            var slider = await _sliderFacade.GetById(id);
            Link = slider.Link;
            Title = slider.Title;
        }

        public async Task<IActionResult> OnPost(long id)
        {
            var result = await _sliderFacade
                .Edit(new EditSliderCommand(id, Title, Link, ImageFile));

            return RedirectAndShowAlert(result, RedirectToPage("Index"));
        }


    }
}
