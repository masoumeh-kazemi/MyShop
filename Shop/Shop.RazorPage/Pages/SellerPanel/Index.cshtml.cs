using Common.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Presentation.Facade.Sellers;
using Shop.Query.Sellers.Inventories.DTOs;

namespace Shop.RazorPage.Pages.SellerPanel
{
    public class IndexModel : PageModel
    {
        public SellerDto Seller { get; set; }
        private readonly ISellerFacade _sellerFacade;

        public IndexModel(ISellerFacade sellerFacade)
        {
            _sellerFacade = sellerFacade;
        }
        public async Task<IActionResult> OnGet()
        {
            var seller = await _sellerFacade.GetByUserId(User.GetUserId());
            if (seller == null)
            {
                return NotFound();
            }

            Seller = seller;
            return Page();
        }
    }
}
