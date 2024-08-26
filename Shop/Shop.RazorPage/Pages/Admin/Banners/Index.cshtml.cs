using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Presentation.Facade.SiteEntities.Banners;
using Shop.Query.SiteEntities.DTOs;

namespace Shop.RazorPage.Pages.Admin.Banners
{
    public class IndexModel : PageModel
    {

        public List<BannerDto> Banners { get; set; }

        private readonly IBannerFacade _bannerFacade;

        public IndexModel(IBannerFacade bannerFacade)
        {
            _bannerFacade = bannerFacade;
        }
        public async Task OnGet()
        {
            var banners = await _bannerFacade.GetList();
            Banners = banners;
        }
    }
}
