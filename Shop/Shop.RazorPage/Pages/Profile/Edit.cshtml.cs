using System.ComponentModel.DataAnnotations;
using Common.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Users.Edit;
using Shop.Application.Users.EditProfile;
using Shop.Domain.UserAgg.Enum;
using Shop.Presentation.Facade.Users;
using Shop.Query.Users.DTOs;
using Shop.RazorPage.Pages.Infrastructure.RazorUtil;

namespace Shop.RazorPage.Pages.Profile
{
    [BindProperties]
    public class EditModel : BaseRazorPage
    {   
        [Display(Name = "نام")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Name { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Family { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Password { get; set; }

        [Display(Name = "شماره تلفن")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string PhoneNumber{ get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Email { get; set; }

        [Display(Name = "جنسیت")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public Gender Gender { get; set; }

        [Display(Name = "تصویر")]
        public IFormFile? ImageFile { get; set; }

        private readonly IUserFacade _userFacade;

        public EditModel(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        public async Task OnGet()
        {
            var user = await _userFacade.GetById(User.GetUserId());
            Name = user.Name;
            Family = user.Family;
            Password = user.Password;
            PhoneNumber = user.PhoneNumber;
            Email = user.Email;
            Gender = user.Gender;
            
            
        }

        public async Task<IActionResult> OnPost()
        {
            var result = await _userFacade.EditProfile(new EditUserProfileCommand(User.GetUserId(), Name,
                Family, PhoneNumber, Email, ImageFile, Gender));

            return RedirectAndShowAlert(result, RedirectToPage("Index"));
        }
    }
}
