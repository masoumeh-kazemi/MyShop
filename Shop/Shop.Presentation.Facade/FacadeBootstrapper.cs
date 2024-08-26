using Microsoft.Extensions.DependencyInjection;
using Shop.Presentation.Facade.Categories;
using Shop.Presentation.Facade.Orders;
using Shop.Presentation.Facade.Products;
using Shop.Presentation.Facade.Roles;
using Shop.Presentation.Facade.Sellers;
using Shop.Presentation.Facade.Sellers.Inventories;
using Shop.Presentation.Facade.SiteEntities.Banners;
using Shop.Presentation.Facade.SiteEntities.ShippingMethods;
using Shop.Presentation.Facade.SiteEntities.Sliders;
using Shop.Presentation.Facade.Users;
using Shop.Presentation.Facade.Users.Addresses;

namespace Shop.Presentation.Facade;

public  class FacadeBootstrapper
{
    public static void Init(IServiceCollection services)
    {
        services.AddScoped<IUserFacade, UserFacade>();
        services.AddScoped<IRoleFacade, RoleFacade>();
        services.AddScoped<IUserAddressFacade, UserAddressFacade>();
        services.AddScoped<ICategoryFacade, CategoryFacade>();
        services.AddScoped<IProductFacade, ProductFacade>();
        services.AddScoped<ISellerFacade, SellerFacade>();
        services.AddScoped<ISellerInventoryFacade, SellerInventoryFacade>();
        services.AddScoped<ISliderFacade, SliderFacade>();
        services.AddScoped<IBannerFacade, BannerFacade>();
        services.AddScoped<IOrderFacade, OrderFacade>();
        services.AddScoped<IShippingMethodFacade, ShippingMethodFacade>();






    }
}