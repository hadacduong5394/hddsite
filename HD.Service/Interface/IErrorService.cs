using HD.Domain.Models;

namespace HD.Service.Interface
{
    public interface IErrorService : IBaseService
    {
        void CreateNew(Error model);
    }
}