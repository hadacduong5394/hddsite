using HD.Domain.Models;
using System.Collections.Generic;

namespace HD.Service.Interface
{
    public interface IProductService : IBaseService
    {
        Product GetBykey(int id);

        void Update(Product product);

        IEnumerable<Product> GetAll();
    }
}