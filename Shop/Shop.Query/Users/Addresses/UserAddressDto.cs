using Common.Query;
using System.ComponentModel.DataAnnotations;

namespace Shop.Query.Users.Addresses;

public class UserAddressDto :  BaseDto
{
    [Display(Name = "استان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Shire { get; set; }

    [Display(Name = "شهر")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string City { get; set; }


    public string PostalCode { get; set; }

    public string PostalAddress { get; set; }

    public string PhoneNumber { get; set; }
    
    public string Name { get; set; }


    public string Family { get; set; }


    public string NationalCode { get; set; }

    public bool ActiveAddress { get; set; }
}