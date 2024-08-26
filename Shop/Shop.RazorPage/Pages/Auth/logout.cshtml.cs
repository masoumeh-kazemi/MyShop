using Azure.Core;
using Common.Application;
using Common.Application.SecurityUtil;
using Common.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Users.DeleteToken;
using Shop.Presentation.Facade.Users;
using Shop.RazorPage.Pages.Infrastructure.RazorUtil;

namespace Shop.RazorPage.Pages.Auth
{
    public class logoutModel : BaseRazorPage
    {
        private readonly IUserFacade _userFacade;

        public logoutModel(IUserFacade userFacade)
        {
            _userFacade = userFacade;
            
        }
        public async Task<IActionResult> OnGet()
        {
            var userId = User.GetUserId();

            var jwtToken = await HttpContext.GetTokenAsync("access_token");
            //if (jwtToken == null)
            //return Page();

            var hashJwtToken = Sha256Hasher.Hash(jwtToken);



            var userToken = await _userFacade.GetUserTokenByJwtToken(hashJwtToken);
            var result = await _userFacade.DeleteToken(new DeleteUserTokenCommand(User.GetUserId(), userToken.Id));

            if (result.Status == OperationResultStatus.Success)
            {
                HttpContext.Response.Cookies.Delete("token");
                HttpContext.Response.Cookies.Delete("refresh-token");
            }

            return RedirectToPage("../Index");
        }



    }
}
