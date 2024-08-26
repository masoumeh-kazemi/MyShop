using System.ComponentModel.DataAnnotations;

namespace Shop.RazorPage.Pages.ViewModel;

public class EditSellerInventoryViewModel
{
    [Display(Name = "آی دی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public long ProductId { get; set; }

    public long SellerId { get; set; }
    public long InventoryId { get; set; }

    [Display(Name = "تعداد")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public int Count { get; set; }

    [Display(Name = "قیمت")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public int Price { get; set; }

    //[Display(Name = "درصد تخفیف")]
    //[Required(ErrorMessage = "{0} را وارد کنید")]
    //public int? DiscountPercentage { get; set; }
}