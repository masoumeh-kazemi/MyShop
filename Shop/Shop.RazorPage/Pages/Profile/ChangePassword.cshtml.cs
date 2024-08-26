using System.ComponentModel.DataAnnotations;
using Common.Application.SecurityUtil;
using Common.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Users.ChangePassword;
using Shop.Presentation.Facade.Users;
using Shop.RazorPage.Pages.Infrastructure.RazorUtil;

namespace Shop.RazorPage.Pages.Profile
{

    [BindProperties]
    public class ChangePasswordModel : BaseRazorPage
    {    
        [Display(Name = "کلمه عبور فعلی")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Display(Name = "کلمه عبور جدید")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "تکرار کلمه عبور جدید")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }

        private readonly IUserFacade _userFacade;

        public ChangePasswordModel(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (NewPassword != ConfirmNewPassword)
            {
                ModelState.AddModelError("CurrentPassword", "کلمه عبور و تکرار آن یکسان نیستند");
                return Page();
            }

            var result = await _userFacade.ChangePassword(new ChangeUserPasswordCommand(User.GetUserId(), CurrentPassword,
                NewPassword));
            return RedirectAndShowAlert(result, RedirectToPage("Index"));
        }
    }
}
