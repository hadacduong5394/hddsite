using System;
using System.Collections.Generic;
using HD.Domain.Models;
using HD.Infrastructure.UnitOfWork;
using HD.Service.Interface;
using HD.Repository.Interface;
using HD.Core;

namespace HD.Service.Implementation
{
    public class TagService : BaseService, ITagService
    {
        private readonly ITagRepository _tagRep;
        public TagService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _tagRep = IoC.Resolve<ITagRepository>();
        }

        public bool CheckTitleContain(string title)
        {
            return _tagRep.CheckContains(n => n.TagTitle.Equals(title));
        }

        public void CreateNew(Tag entity)
        {
            _tagRep.CreateNew(entity);
        }

        public Tag GetByKey(string title)
        {
            return _tagRep.GetSingleById(title);
        }

        public IList<Tag> GetTags(string keyWord, int currentPage, int pageSize, out int total)
        {
            return _tagRep.GetTags(keyWord, currentPage, pageSize, out total);
        }
    }
}