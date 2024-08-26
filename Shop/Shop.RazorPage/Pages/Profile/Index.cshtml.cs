using System.ComponentModel.DataAnnotations;
using Common.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Presentation.Facade.Users;
using Shop.Query.Users.DTOs;

namespace Shop.RazorPage.Pages.Profile
{
    public class IndexModel : PageModel
    {

        public UserDto UserDto { get; set; }

        private readonly IUserFacade _userFacade;

        public IndexModel(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }
        public async Task OnGet()
        {
            var user = await _userFacade.GetById(User.GetUserId());
            if (user == null)
            {
                return;
            }

            UserDto = user;
        }
    }
}
