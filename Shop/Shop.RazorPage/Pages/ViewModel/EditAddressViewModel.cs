using System.ComponentModel.DataAnnotations;

namespace Shop.RazorPage.Pages.ViewModel;

public class EditAddressViewModel
{
    public long AddressId { get; set; }
    [Display(Name = "استان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Shire { get; set; }

    [Display(Name = "شهر")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string City { get; set; }

    [Display(Name = "کد پستی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string PostalCode { get; set; }

    [Display(Name = "آدرس پستی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string PostalAddress { get; set; }

    [Display(Name = "شماره تماس")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string PhoneNumber { get; set; }

    [Display(Name = "نام")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Name { get; set; }

    [Display(Name = "نام خانوادگی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Family { get; set; }

    [Display(Name = "کد ملی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string NationalCode { get; set; }

    public bool ActiveAddress { get; set; }
}