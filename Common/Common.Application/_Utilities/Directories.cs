namespace Common.Application._Utilities;

public class Directories
{
    public const string ProductImages = "wwwroot/images/products";
    public const string ProductGalleryImage = "wwwroot/images/products/gallery";

    public const string BannerImages = "wwwroot/images/banners";
    public const string SliderImages = "wwwroot/images/sliders";

    public const string UserAvatars = "wwwroot/images/users/avatar";



    public static string GetUserAvatar(string imageName)
    {
        return $"{UserAvatars}/{imageName}";
    }

    public static string GetBannerImage(string imageName)
    {
        var bannersDirectories = "/images/banners/";
        return $"{bannersDirectories}{imageName}";
    }


    public static string GetSliderImage(string imageName)
    {
        var sliderDirectory = "/images/sliders/";
        return $"{sliderDirectory}{imageName}";
    }

    public static string GetProductImage(string imageName)
    {
        var productDirectory = "/images/products/";
        return $"{productDirectory}{imageName}";
    }

    public static string GetProductGalleryImage(string imageName)
    {
        var imageGalleryDirectory = "/images/products/gallery/";
        return $"{imageGalleryDirectory}{imageName}";
    }

}