using System.ComponentModel.DataAnnotations;
using Common.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Sellers.Create;
using Shop.Presentation.Facade.Sellers;
using Shop.RazorPage.Pages.Infrastructure.RazorUtil;

namespace Shop.RazorPage.Pages.Auth
{
    [BindProperties]
    public class SellerRegisterModel : BaseRazorPage
    {
        [Display(Name = "نام فروشگاه")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string ShopName { get; set; }

        [Display(Name = "کد ملی")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string NationalCode { get; set; }

        private readonly ISellerFacade _sellerFacade;

        public SellerRegisterModel(ISellerFacade sellerFacade)
        {
            _sellerFacade = sellerFacade; 
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var result = await _sellerFacade.Create(new CreateSellerCommand(User.GetUserId(), NationalCode, ShopName));
            return RedirectAndShowAlert(result, RedirectToPage("../Index"));
        }
    }
}
