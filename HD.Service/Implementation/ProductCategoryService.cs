using System;
using System.Collections.Generic;
using HD.Domain.Models;
using HD.Infrastructure.UnitOfWork;
using HD.Service.Interface;
using HD.Repository.Interface;
using HD.Core;

namespace HD.Service.Implementation
{
    public class ProductCategoryService : BaseService, IProductCategoryService
    {
        private readonly IProductCategoryRepository _proCatRepo;

        public ProductCategoryService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _proCatRepo = IoC.Resolve<IProductCategoryRepository>();
        }

        public void CreateNew(ProductCategory entity)
        {
            _proCatRepo.CreateNew(entity);
        }

        public void Delete(int id)
        {
            _proCatRepo.Delete(id);
        }

        public IList<ProductCategory> GetByFilter(string keyWord, int? typeId, int? parentId, int currentPage, int pageSize, out int total)
        {
            return _proCatRepo.GetByFilter(keyWord, typeId, parentId, currentPage, pageSize, out total);
        }

        public ProductCategory GetBykey(int id)
        {
            return _proCatRepo.GetSingleById(id);
        }

        public IEnumerable<ProductCategory> GetParents()
        {
            return _proCatRepo.GetMulti(n =>n.ParentId == null);
        }

        public void Update(ProductCategory entity)
        {
            _proCatRepo.Update(entity);
        }
    }
}