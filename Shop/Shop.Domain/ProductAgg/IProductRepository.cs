using Common.Domain.Repository;

namespace Shop.Domain.ProductAgg;

public interface IProductRepository : IBaseRepository<Product>
{
    void Delete(Product product);
}