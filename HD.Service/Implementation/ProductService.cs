using HD.Domain.Models;
using HD.Infrastructure.UnitOfWork;
using HD.Repository.Interface;
using HD.Service.Interface;
using System.Collections.Generic;

namespace HD.Service.Implementation
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository _productRepo;

        public ProductService(IUnitOfWork unitOfWork, IProductRepository productRepo) : base(unitOfWork)
        {
            this._productRepo = productRepo;
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepo.GetAll();
        }

        public Product GetBykey(int id)
        {
            return _productRepo.GetSingleById(id);
        }

        public void Update(Product product)
        {
            _productRepo.Update(product);
        }
    }
}