using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Presentation.Facade.Sellers;
using Shop.Query.Sellers.Inventories.DTOs;
using Shop.RazorPage.Pages.Infrastructure.RazorUtil;

namespace Shop.RazorPage.Pages.Admin.Sellers
{
    public class IndexModel : BaseRazorFilter<SellerFilterParam>
    {
        public SellerFilterResult Sellers { get; set; }

        private readonly ISellerFacade _sellerFacade;

        public IndexModel(ISellerFacade sellerFacade)
        {
            _sellerFacade = sellerFacade;
        }
        public async Task OnGet()
        {
            var seller = await _sellerFacade.GetByFilter(FilterParam);
            Sellers = seller;
        }
    }
}
