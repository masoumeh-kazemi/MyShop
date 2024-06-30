using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Products;
using Shop.Application.Users;
using Shop.Domain.ProductAgg.Services;
using Shop.Domain.UserAgg.Services;
using Shop.Infrastructure;

namespace Shop.Config;

public static class ShopBootstrapper
{
    public static void RegisterShopDependency(this IServiceCollection services, string connectionString)
    {
        InfrastructureBootstrapper.Init(services, connectionString);
        services.AddScoped<IUserDomainService, UserDomainService>();
        services.AddScoped<IProductDomainService, ProductDomainService>();

    }
}