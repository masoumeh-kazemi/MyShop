using Shop.Domain.UserAgg;
using Shop.Infrastructure._Utilities;

namespace Shop.Infrastructure.Persistent.EF.UserAgg;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(ShopContext context) : base(context)
    {
    }
}