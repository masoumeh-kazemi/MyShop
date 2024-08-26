using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Presentation.Facade.Users;
using Shop.Query.Users.DTOs;
using Shop.RazorPage.Pages.Infrastructure.RazorUtil;

namespace Shop.RazorPage.Pages.Admin.Users
{
    [Authorize]
    public class IndexModel : BaseRazorFilter<UserFilterParam>
    {
        public UserFilterResult Users { get; set; }
        private readonly IUserFacade _userFacade;

        public IndexModel(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }
        public async Task OnGet()
        {
             Users = await _userFacade.GetByFilter(FilterParam);

        }
    }
}
