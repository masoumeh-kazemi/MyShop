using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shop.Domain.CategoryAgg;
using Shop.Domain.OrderAgg;
using Shop.Domain.ProductAgg;
using Shop.Domain.RoleAgg;
using Shop.Domain.SellerAgg;
using Shop.Domain.SiteEntities;
using Shop.Domain.UserAgg;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Infrastructure.Persistent.EF;
using Shop.Infrastructure.Persistent.EF.CategoryAgg;
using Shop.Infrastructure.Persistent.EF.OrderAgg;
using Shop.Infrastructure.Persistent.EF.ProductAgg;
using Shop.Infrastructure.Persistent.EF.RoleAgg;
using Shop.Infrastructure.Persistent.EF.SellerAgg;
using Shop.Infrastructure.Persistent.EF.SiteEntities.Repositories;
using Shop.Infrastructure.Persistent.EF.SiteEntities.ShippingMethods;
using Shop.Infrastructure.Persistent.EF.UserAgg;

namespace Shop.Infrastructure;

public static class InfrastructureBootstrapper
{
    public static void Init(IServiceCollection services, string connectionString)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ISellerRepository, SellerRepository>();
        services.AddScoped<IBannerRepository, BannerRepository>();
        services.AddScoped<ISliderRepository, SliderRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IShippingMethodRepository, ShippingMethodRepository>();



        services.AddTransient(_ => new DapperContext(connectionString));

        services.AddDbContext<ShopContext>(option =>
        {
            option.UseSqlServer(connectionString);

        });
    }
}