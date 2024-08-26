using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Domain.RoleAgg.Enum;
using Shop.Presentation.Facade.Roles;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Shop.Application.Roles.Edit;
using Shop.RazorPage.Pages.Infrastructure.RazorUtil;

namespace Shop.RazorPage.Pages.Admin.Roles
{
    [BindProperties]
    public class EditModel : BaseRazorPage
    {

        [DisplayName("عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }
        public List<Permission> Permissions { get; set; }
        private readonly IRoleFacade _roleFacade;

        public EditModel(IRoleFacade roleFacade)
        {
            _roleFacade = roleFacade;
        }
        public async Task OnGet(long id)
        {
            var role = await _roleFacade.GetById(id);
            if (role == null)
            {
             return;
            }

            Title = role.Title;

            Permissions = role.RolePermissions;
            
        }

        public async Task<IActionResult> OnPost(long id, string[] permissions)
        {
            var permissionEnum = permissions.Select(p => (Permission)Enum.Parse(typeof(Permission), p));
            var permissionList = permissionEnum.ToList();
            var result = await _roleFacade.Edit(new EditRoleCommand(id, Title, permissionList));
            return RedirectAndShowAlert(result, RedirectToPage("Index"));
        }
    }
}
