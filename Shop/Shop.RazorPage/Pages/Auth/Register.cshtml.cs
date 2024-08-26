using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Users.Register;
using Shop.Presentation.Facade.Users;

namespace Shop.RazorPage.Pages.Auth
{
    [BindProperties]
    public class RegisterModel : PageModel
    {        
        [Display(Name = "شماره تلفن")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [MinLength(11, ErrorMessage = "شماره تلفن باید 11 کارکتر باشد")]
        [MaxLength(11, ErrorMessage = "شماره تلفن باید 11 کارکتر باشد")]

        public string PhoneNumber { get; set; }

        [Display(Name = "کلمه  عبور")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public void OnGet()
        {
        }

        private readonly IUserFacade _userFacade;

        public RegisterModel(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        public async Task<IActionResult> OnPost()
        {
            var user = await _userFacade.Register(new UserRegisterCommand(PhoneNumber, Password));
            return RedirectToPage("Login");
        }

    }
}
