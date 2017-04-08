using System;
using System.Collections.Generic;
using HD.Domain.Models;
using HD.Infrastructure.UnitOfWork;
using HD.Service.Interface;
using HD.Repository.Interface;
using HD.Core;
using System.Linq;

namespace HD.Service.Implementation
{
    public class ArticleCategoryService : BaseService, IArticleCategoryService
    {
        private readonly IArticleCategoryRepository _artRp;
        public ArticleCategoryService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _artRp = IoC.Resolve<IArticleCategoryRepository>();
        }

        public void CreateNew(ArcticleCategory arcticleCat)
        {
            _artRp.CreateNew(arcticleCat);
        }

        public void Delete(int articleCatId)
        {
            _artRp.Delete(articleCatId);
        }

        public bool ExistChilds(int parentId)
        {
            return _artRp.ExistChilds(parentId);
        }

        public IList<ArcticleCategory> GetByFilter(int? catId, string name, int currentPage, int pageSize, out int total)
        {
            return _artRp.GetByFilter(catId, name, currentPage, pageSize, out total);
        }

        public ArcticleCategory GetByKey(int id)
        {
            return _artRp.GetSingleById(id);
        }

        public List<ArcticleCategory> GetChilds(int? parentId)
        {
            if (parentId.HasValue)
            {
                return _artRp.GetMulti(n => n.ParentId == parentId.Value).ToList();
            }

            return _artRp.GetMulti(n => n.ParentId != null).ToList();
        }

        public List<ArcticleCategory> GetParents()
        {
            return _artRp.GetParents();
        }

        public void Update(ArcticleCategory arcticleCat)
        {
            _artRp.Update(arcticleCat);
        }
    }
}