using HD.Domain.Models;
using HD.Infrastructure.UnitOfWork;
using HD.Repository.Interface;
using HD.Service.Interface;

namespace HD.Service.Implementation
{
    public class ErrorService : BaseService, IErrorService
    {
        private readonly IErrorRepository _errorRepository;

        public ErrorService(IErrorRepository errorRepository, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this._errorRepository = errorRepository;
        }

        public void CreateNew(Error model)
        {
            _errorRepository.CreateNew(model);
        }
    }
}