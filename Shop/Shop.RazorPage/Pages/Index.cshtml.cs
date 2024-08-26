using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Domain.SiteEntities;
using Shop.Presentation.Facade.Products;
using Shop.Presentation.Facade.SiteEntities.Banners;
using Shop.Presentation.Facade.SiteEntities.Sliders;
using Shop.Query.Products.DTOs;
using Shop.Query.SiteEntities.DTOs;

namespace Shop.RazorPage.Pages
{
    public class IndexModel : PageModel
    {
        public List<SliderDto> Sliders { get; set; }
        public List<BannerDto> Banners { get; set; }

        public List<ProductShopDto> LatestProducts { get; set; }
        public List<ProductShopDto> TopVisionProducts { get; set; }
        public List<ProductShopDto> SpecialProductsResult { get; set; }
        private readonly ILogger<IndexModel> _logger;
        private readonly IBannerFacade _bannerFacade;
        private readonly ISliderFacade _sliderFacade;
        private readonly IProductFacade _productFacade;
        public IndexModel(ILogger<IndexModel> logger, IBannerFacade bannerFacade, ISliderFacade sliderFacade, IProductFacade productFacade)
        {
            _logger = logger;
            _bannerFacade = bannerFacade;
            _sliderFacade = sliderFacade;
            _productFacade = productFacade;
        }

        public async Task OnGet()
        {
            var slider = await _sliderFacade.GetByList();
            var banners = await _bannerFacade.GetList();
            Banners = banners;
            Sliders = slider;
            var latestProducts = await _productFacade.GetForShop(new ProductShopFilterParam()
            {
                PageId = 1,
                Take = 8,
                SearchOrderBy = ProductSearchOrderBy.Latest,
                OnlyAvailableProducts = true
            });
            LatestProducts = latestProducts.Data;

            var topVisionProducts = await _productFacade.GetForShop(new ProductShopFilterParam()
            {
                PageId = 1,
                Take = 8,
                OnlyAvailableProducts = true

            });
            TopVisionProducts = topVisionProducts.Data;
            var specialProductsResult = await _productFacade.GetForShop(new ProductShopFilterParam()
            {
                PageId = 1,
                Take = 8,
                //JustHasDiscount = true,
                OnlyAvailableProducts = true,
                SearchOrderBy = ProductSearchOrderBy.Expensive,


            });
            SpecialProductsResult = specialProductsResult.Data;



        }



    }
}