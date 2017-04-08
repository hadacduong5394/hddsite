using HD.Core;
using HD.Domain.Models;
using HD.Infrastructure.UnitOfWork;
using HD.Repository.Interface;
using HD.Service.Interface;
using System.Collections.Generic;
using System;

namespace HD.Service.Implementation
{
    public class ArticleService : BaseService, IArticleService
    {
        private readonly IArticleRepository _artRp;

        public ArticleService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _artRp = IoC.Resolve<IArticleRepository>();
        }

        public void CreateNew(Arcticle article)
        {
            _artRp.CreateNew(article);
        }

        public void Delete(int articleId)
        {
            _artRp.Delete(articleId);
        }

        public IEnumerable<Arcticle> GetByFilter(string keyWord, int? parentCatId, int? catId, int currentPage, int pageSize, out int total)
        {
            return _artRp.GetByFilter(keyWord, parentCatId, catId, currentPage, pageSize, out total);
        }

        public Arcticle GetByKey(int id)
        {
            return _artRp.GetSingleById(id);
        }

        public void Update(Arcticle article)
        {
            _artRp.Update(article);
        }
    }
}