using System.ComponentModel.DataAnnotations;
using Common.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Users.Addresses.Create;
using Shop.Presentation.Facade.Users.Addresses;
using Shop.RazorPage.Pages.Infrastructure.RazorUtil;

namespace Shop.RazorPage.Pages.Profile.Addresses
{
    public class AddModel : BaseRazorPage
    {

        [Display(Name = "استان")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Shire { get; set; }

        [Display(Name = "شهر")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string City { get; set; }

        [Display(Name = "کد پستی")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string PostalCode { get; set; }

        [Display(Name = "آدرس پستی")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string PostalAddress { get; set; }

        [Display(Name = "شماره تماس")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string PhoneNumber { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Name { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Family { get; set; }

        [Display(Name = "کد ملی")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string NationalCode { get; set; }

        public bool ActiveAddress { get; set; }
        private readonly IUserAddressFacade _userAddressFacade;

        public AddModel(IUserAddressFacade userAddressFacade)
        {
            _userAddressFacade = userAddressFacade;
        }
        public void OnGet()
        {
        }


        public async Task<IActionResult> OnPost()
        {
            var result = await _userAddressFacade.Create(new CreateUserAddressCommand(User.GetUserId(),
                Shire, City, PostalCode, PostalAddress, PhoneNumber, Name, Family, NationalCode
            ));
            return RedirectAndShowAlert(result, RedirectToPage("Index"));
        }
    }
}
