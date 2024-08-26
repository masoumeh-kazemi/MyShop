using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.SiteEntities.Banners.Edit;
using Shop.Domain.SiteEntities.Enum;
using Shop.Presentation.Facade.SiteEntities.Banners;
using Shop.RazorPage.Pages.Infrastructure.RazorUtil;

namespace Shop.RazorPage.Pages.Admin.Banners
{
    [BindProperties]
    public class EditModel : BaseRazorPage
    {
        [Display(Name = "لینک")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Link { get; set; }

        [Display(Name = "موقعیت")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public BannerPosition Position { get; set; }

        [Display(Name = "عکس")]
        public IFormFile? ImageFile { get; set; }

        private readonly IBannerFacade _bannerFacade;

        public EditModel(IBannerFacade bannerFacade)
        {
            _bannerFacade = bannerFacade;
        }
        public async Task OnGet(long id)
        {
            var banner = await _bannerFacade.GetById(id);
            Link = banner.Link;
            Position = banner.Position;
        }

        public async Task<IActionResult> OnPost(long id)
        {
            var result = await _bannerFacade
                .Edit(new EditBannerCommand(id, Link, ImageFile, Position));

            return RedirectAndShowAlert(result, RedirectToPage("Index"));

        }
    }
}
