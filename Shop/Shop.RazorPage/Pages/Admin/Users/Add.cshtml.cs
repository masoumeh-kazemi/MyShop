using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Users.Create;
using Shop.Domain.UserAgg.Enum;
using Shop.Presentation.Facade.Users;
using Shop.RazorPage.Pages.Infrastructure.RazorUtil;

namespace Shop.RazorPage.Pages.Admin.Users
{
    [BindProperties]
    public class AddModel : BaseRazorPage
    {        
        [Display(Name = "نام")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Name { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Family { get; set; }

        [Display(Name = "شماره تلفن")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string PhoneNumber { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Email { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Password { get; set; }

        [Display(Name = "تصویر")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public IFormFile? AvatarImage { get; set; }

        [Display(Name = "کاربر فعال باشد؟")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public bool IsActive { get; set; }

        [Display(Name = "جنسیت")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public Gender Gender { get; set; }

        public List<long> RoleIds { get; set; }

        private readonly IUserFacade _userFacade;
        public AddModel(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {

            var result = await _userFacade.Create(new CreateUserCommand(Name, Family,
                PhoneNumber, Email, Password, AvatarImage, IsActive, Gender, RoleIds));

            return RedirectAndShowAlert(result, RedirectToPage("Index"));
        }
    }
}
