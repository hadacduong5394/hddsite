using HD.Domain.Models;
using System.Collections.Generic;

namespace HD.Service.Interface
{
    public interface ITagService : IBaseService
    {
        IList<Tag> GetTags(string keyWord, int currentPage, int pageSize, out int total);

        bool CheckTitleContain(string title);

        void CreateNew(Tag entity);

        Tag GetByKey(string title);
    }
}