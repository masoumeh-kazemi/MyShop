using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Domain.UserAgg.Enum;
using System.ComponentModel.DataAnnotations;
using Shop.Application.Users.Edit;
using Shop.Presentation.Facade.Users;
using Shop.RazorPage.Pages.Infrastructure.RazorUtil;

namespace Shop.RazorPage.Pages.Admin.Users
{

    [BindProperties]
    public class EditModel : BaseRazorPage
    {
        [Display(Name = "نام")]
        //[Required(ErrorMessage = "{0} را وارد کنید")]
        public string? Name { get; set; }

        [Display(Name = "نام خانوادگی")]
        //[Required(ErrorMessage = "{0} را وارد کنید")]
        public string? Family { get; set; }

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
        //[Required(ErrorMessage = "{0} را وارد کنید")]
        public IFormFile? AvatarImage { get; set; }

        [Display(Name = "کاربر فعال باشد؟")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public bool IsActive { get; set; }

        [Display(Name = "جنسیت")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public Gender Gender { get; set; }

        public List<long> RoleIds { get; set; }
        private readonly IUserFacade _userFacade;

        public EditModel(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }
        public async Task OnGet(long id)
        {
            var user = await _userFacade.GetById(id);
            Name = user.Name;
            Family = user.Family;
            PhoneNumber = user.PhoneNumber;
            Email = user.Email;
            IsActive = user.IsActive;
            Gender = user.Gender;
        }


        public async Task<IActionResult> OnPost(long id)
        {
            var result = await _userFacade.Edit(new EditUserCommand(id, Name, Family,
                PhoneNumber, Email, AvatarImage, IsActive, Gender, RoleIds));

            return RedirectAndShowAlert(result, RedirectToPage("Index"));
        }
    }
}
