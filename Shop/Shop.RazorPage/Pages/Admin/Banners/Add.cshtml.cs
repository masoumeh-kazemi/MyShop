using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.SiteEntities.Banners.Create;
using Shop.Domain.SiteEntities.Enum;
using Shop.Presentation.Facade.SiteEntities.Banners;
using Shop.RazorPage.Pages.Infrastructure.RazorUtil;

namespace Shop.RazorPage.Pages.Admin.Banners
{
    [BindProperties]
    public class AddModel : BaseRazorPage
    {        
        [Display(Name = "لینک")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Link { get; set; }

        [Display(Name = "موقعیت")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public BannerPosition Position { get; set; }

        [Display(Name = "عکس")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public IFormFile ImageFile { get; set; }

        private readonly IBannerFacade _bannerFacade;

        public AddModel(IBannerFacade bannerFacade)
        {
            _bannerFacade = bannerFacade;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var result = await _bannerFacade
                .Create(new CreateBannerCommand(Link, ImageFile, Position));

            return RedirectAndShowAlert(result, RedirectToPage("Index"));
        }
    }
}
