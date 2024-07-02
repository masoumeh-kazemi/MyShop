using System.Security.Claims;
using Common.Application.SecurityUtil;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Presentation.Facade.Users;

namespace Shop.RazorPage.Pages.Auth
{
    public class LoginModel : PageModel
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

        private readonly IUserFacade _userFacade;

        public LoginModel(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var user = await _userFacade.GetByPhoneNumber(PhoneNumber);
            var isPasswordCorrect = Sha256Hasher.IsCompare(user.Password, Password);
            if (isPasswordCorrect==false)
            {
                ModelState.AddModelError("PhoneNumber", "?????? ?? ??? ?????? ???? ???");
                return Page();
            }

            var claims = new List<Claim>()
            {
                //new Claim()
            };

            return RedirectToPage("Index");
        }
    }
}
