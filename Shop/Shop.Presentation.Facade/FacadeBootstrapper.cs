using Microsoft.Extensions.DependencyInjection;
using Shop.Presentation.Facade.Roles;
using Shop.Presentation.Facade.Users;

namespace Shop.Presentation.Facade;

public  class FacadeBootstrapper
{
    public static void Init(IServiceCollection services)
    {
        services.AddScoped<IUserFacade, UserFacade>();
        services.AddScoped<IRoleFacade, RoleFacade>();

    }
}