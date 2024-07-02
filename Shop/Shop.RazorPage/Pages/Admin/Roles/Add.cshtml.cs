using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Roles.Create;
using Shop.Domain.RoleAgg.Enum;
using Shop.Presentation.Facade.Roles;
using Shop.RazorPage.Pages.Infrastructure.RazorUtil;

namespace Shop.RazorPage.Pages.Admin.Roles
{
    public class AddModel : BaseRazorPage
    {
        [BindProperty]        
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Title { get; set; }

        private readonly IRoleFacade _roleFacade;
        public AddModel(IRoleFacade roleFacade)
        {
            _roleFacade = roleFacade;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string[] permissions)
        {
            var permissionEnum = permissions.Select(p => (Permission)Enum.Parse(typeof(Permission), p));
            var permissionList = permissionEnum.ToList();
            var result = await _roleFacade
                .Create(new CreateRoleCommand(Title, permissionList));

            return RedirectAndShowAlert(result, RedirectToPage("Index"));
        }
    }
}
