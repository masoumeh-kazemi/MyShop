using Common.Domain.Repository;

namespace Shop.Domain.OrderAgg;

public interface IOrderRepository : IBaseRepository<Order>
{
    Task<Order?> GetCurrentOrder(long userId);
}

