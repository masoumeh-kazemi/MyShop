using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Presentation.Facade.SiteEntities.Sliders;
using Shop.Query.SiteEntities.DTOs;

namespace Shop.RazorPage.Pages.Admin.Sliders
{
    public class IndexModel : PageModel
    {
        public List<SliderDto> Sliders { get; set; }
        private readonly ISliderFacade _sliderFacade;

        public IndexModel(ISliderFacade sliderFacade)
        {
            _sliderFacade = sliderFacade;
        }
        public async Task OnGet()
        {
            var sliders =await _sliderFacade.GetByList();
            Sliders = sliders;
        }

        
    }
}
