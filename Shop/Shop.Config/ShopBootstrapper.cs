using System.Reflection;
using Common.Application;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Categories;
using Shop.Application.Products;
using Shop.Application.Sellers;
using Shop.Application.Users;
using Shop.Application.Users.Register;
using Shop.Domain.CategoryAgg.Services;
using Shop.Domain.ProductAgg.Services;
using Shop.Domain.SellerAgg.Services;
using Shop.Domain.UserAgg.Services;
using Shop.Infrastructure;
using Shop.Presentation.Facade;
using Shop.Query.Users.GetByPhoneNumber;

namespace Shop.Config;

public static class ShopBootstrapper
{
    public static void RegisterShopDependency(this IServiceCollection services, string connectionString)
    {
        InfrastructureBootstrapper.Init(services, connectionString);
        CommonBootstrapper.Init(services);
        FacadeBootstrapper.Init(services);
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(typeof(UserRegisterCommand).GetTypeInfo().Assembly));

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(typeof(GetUserByPhoneNumberQuery).GetTypeInfo().Assembly));

        services.AddScoped<IUserDomainService, UserDomainService>();
        services.AddScoped<IProductDomainService, ProductDomainService>();
        services.AddScoped<ICategoryDomainService, CategoryDomainService>();
        services.AddScoped<ISellerDomainService, SellerDomainService>();





    }
}