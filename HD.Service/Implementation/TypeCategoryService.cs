using HD.Core;
using HD.Domain.Models;
using HD.Infrastructure.UnitOfWork;
using HD.Repository.Interface;
using HD.Service.Interface;
using System.Collections.Generic;
using System;

namespace HD.Service.Implementation
{
    public class TypeCategoryService : BaseService, ITypeCategoryService
    {
        private readonly ITypeCategoryRepository _typeCatRepo;
        private readonly IProductCategoryRepository _proCatRp;

        public TypeCategoryService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _typeCatRepo = IoC.Resolve<ITypeCategoryRepository>();
            _proCatRp = IoC.Resolve<IProductCategoryRepository>();
        }

        public void CreateNew(TypeCategory entity)
        {
            _typeCatRepo.CreateNew(entity);
        }

        public void Delete(int id)
        {
            _typeCatRepo.Delete(id);
        }

        public IList<TypeCategory> GetByfilter(string name, int currentPage, int pageSize, out int total)
        {
            return _typeCatRepo.GetByfilter(name, currentPage, pageSize, out total);
        }

        public TypeCategory GetBykey(int id)
        {
            return _typeCatRepo.GetSingleById(id);
        }

        public void Update(TypeCategory entity)
        {
            _typeCatRepo.Update(entity);
        }

        public bool CheckContainProductCategory(int typeId)
        {
            return _proCatRp.CheckContains(n => n.TypeId == typeId);
        }

        public IEnumerable<TypeCategory> GetAll()
        {
            return _typeCatRepo.GetAll();
        }
    }
}