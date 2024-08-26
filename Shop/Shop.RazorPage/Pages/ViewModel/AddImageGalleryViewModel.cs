using System.ComponentModel.DataAnnotations;

namespace Shop.RazorPage.Pages.ViewModel;

public class AddImageGalleryViewModel
{
    [Display(Name = "تصویر")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public IFormFile ImageFile { get; set; }

    [Display(Name = "ترتیب نمایش")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public int Sequence { get; set; }
}