using HD.Domain.Models;
using HD.Infrastructure.Repository;

namespace HD.Repository.Interface
{
    public interface IErrorRepository : IRepositoryBase<Error, int>
    {
    }
}