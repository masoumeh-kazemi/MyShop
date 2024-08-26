using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Sellers.Edit;
using Shop.Domain.SellerAgg;
using Shop.Presentation.Facade.Sellers;
using Shop.RazorPage.Pages.Infrastructure.RazorUtil;

namespace Shop.RazorPage.Pages.Admin.Sellers
{
    [BindProperties]
    public class EditModel : BaseRazorPage
    {
        [Display(Name = "کد ملی")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string NationalCode { get; set; }

        [Display(Name = "نام فروشگاه")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string ShopName { get; set; }

        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public SellerStatus Status { get; set; }

        private readonly ISellerFacade _sellerFacade;

        public EditModel(ISellerFacade sellerFacade)
        {
            _sellerFacade = sellerFacade;
        }
        public async Task OnGet(long id)
        {
            var seller = await _sellerFacade.GetById(id);
            if (seller == null)
            {
                return;
            }

            NationalCode = seller.NationalCode;
            Status = seller.Status;
            ShopName = seller.ShopName;

        }

        public async Task<IActionResult> OnPost(long id)
        {
            var result = await _sellerFacade
                .Edit(new EditSellerCommand(id, NationalCode, ShopName, Status));

            return RedirectAndShowAlert(result, RedirectToPage("Index"));
        }
    }
}
