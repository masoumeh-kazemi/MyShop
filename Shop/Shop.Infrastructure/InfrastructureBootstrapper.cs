using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shop.Domain.ProductAgg;
using Shop.Domain.UserAgg;
using Shop.Infrastructure.Persistent.EF;
using Shop.Infrastructure.Persistent.EF.ProductAgg;
using Shop.Infrastructure.Persistent.EF.UserAgg;

namespace Shop.Infrastructure;

public static class InfrastructureBootstrapper
{
    public static void Init(IServiceCollection services, string connectionString)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddDbContext<ShopContext>(option =>
        {
            option.UseSqlServer(connectionString);

        });
    }
}