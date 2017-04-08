using HD.Domain.Models;
using HD.Infrastructure.DBFactory;
using HD.Infrastructure.Repository;
using HD.Repository.Interface;

namespace HD.Repository.Implementation
{
    public class ErrorRepository : RepositoryBase<Error, int>, IErrorRepository
    {
        public ErrorRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}