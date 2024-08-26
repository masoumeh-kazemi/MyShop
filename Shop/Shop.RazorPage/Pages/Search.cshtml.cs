using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Presentation.Facade.Products;
using Shop.Query.Products.DTOs;
using Shop.RazorPage.Pages.Infrastructure.RazorUtil;

namespace Shop.RazorPage.Pages
{
    public class SearchModel : PageModel
    {
        public ProductShopResult FilterResult { get; set; }

        private readonly IProductFacade _productFacade;

        public SearchModel(IProductFacade productFacade)
        {
            _productFacade = productFacade;
        }
        public async Task OnGet(int pageId = 1, string q = "", string category = "", bool haveDiscount = false, bool justAvailableProducts = true)
        {
            FilterResult = await _productFacade.GetForShop(new ProductShopFilterParam()
            {
                Take = 1,
                OnlyAvailableProducts = justAvailableProducts,
                JustHasDiscount = haveDiscount,
                PageId = pageId,
                Search = q,
                CategorySlug = category,
                SearchOrderBy = ProductSearchOrderBy.Latest
            });
        }
    }
}
