using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Presentation.Facade.Roles;
using Shop.Query.Roles.DTOs;

namespace Shop.RazorPage.Pages.Admin.Roles
{
    public class IndexModel : PageModel
    {

        public List<RoleDto> Roles { get; set; }
        private readonly IRoleFacade _roleFacade;

        public IndexModel(IRoleFacade roleFacade)
        {
            _roleFacade = roleFacade;
        }
        public async Task<IActionResult> OnGet()
        {
            var roles = await _roleFacade.GetList();
            Roles = roles;
            return Page();  
        }


    }
}
