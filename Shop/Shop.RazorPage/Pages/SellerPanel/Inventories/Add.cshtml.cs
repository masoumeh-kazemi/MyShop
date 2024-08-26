using System.ComponentModel.DataAnnotations;
using Common.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Sellers.Inventories.Create;
using Shop.Presentation.Facade.Sellers;
using Shop.Presentation.Facade.Sellers.Inventories;
using Shop.RazorPage.Pages.Infrastructure.RazorUtil;

namespace Shop.RazorPage.Pages.SellerPanel.Inventories
{
    [BindProperties]
    public class AddModel : BaseRazorPage
    {
        [Display(Name = "آی دی")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public long ProductId { get; set; }

        [Display(Name = "تعداد")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public int Count { get; set; }

        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public int Price { get; set; }

        //[Display(Name = "درصد تخفیف")]
        //[Required(ErrorMessage = "{0} را وارد کنید")]
        //[Range(0, 0, ErrorMessage = "درصد تخفیف نامعتبر است")]
        //public int? DiscountPercentage { get; set; }
        private readonly ISellerInventoryFacade _inventoryFacade;
        private readonly ISellerFacade _sellerFacade;


        public AddModel(ISellerInventoryFacade inventoryFacade, ISellerFacade sellerFacade)
        {
            _inventoryFacade = inventoryFacade;
            _sellerFacade = sellerFacade;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(long sellerId)
        {
            var seller = await _sellerFacade.GetByUserId(User.GetUserId());
            if (seller == null)
                return Redirect("/");

            var result = await _inventoryFacade
                .Create(new CreateSellerInventoryCommand(seller.Id, ProductId, Count, Price, 0));

            return RedirectAndShowAlert(result, RedirectToPage("Index"));
        }
    }
}
