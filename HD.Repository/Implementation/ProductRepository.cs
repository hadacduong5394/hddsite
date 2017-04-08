using HD.Domain.Models;
using HD.Infrastructure.DBFactory;
using HD.Infrastructure.Repository;
using HD.Repository.Interface;

namespace HD.Repository.Implementation
{
    public class ProductRepository : RepositoryBase<Product, int>, IProductRepository
    {
        public ProductRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        //public IEnumerable<Product> GetAll()
        //{
        //    var query = from a in DbContext.Products select a;

        //    return query.OrderByDescending(n => n.Id);
        //}
    }
}