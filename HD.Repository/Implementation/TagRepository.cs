using System.Linq;
using System.Collections.Generic;
using HD.Domain.Models;
using HD.Infrastructure.DBFactory;
using HD.Infrastructure.Repository;
using HD.Repository.Interface;

namespace HD.Repository.Implementation
{
    public class TagRepository : RepositoryBase<Tag, string>, ITagRepository
    {
        public TagRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IList<Tag> GetTags(string keyWord, int currentPage, int pageSize, out int total)
        {
            var query = from a in DbContext.Tags where a.TagName.Contains(keyWord) select a;

            query = query.OrderByDescending(n => n.CreateDate);

            total = query.Count();

            var lst = query.Skip(currentPage * pageSize).Take(pageSize).ToList();

            return lst;
        }
    }
}