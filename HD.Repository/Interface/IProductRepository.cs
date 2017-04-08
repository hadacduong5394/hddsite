using HD.Domain.Models;
using HD.Infrastructure.Repository;

namespace HD.Repository.Interface
{
    public interface IProductRepository : IRepositoryBase<Product, int>
    {
        //IEnumerable<Product> GetAll();
    }
}