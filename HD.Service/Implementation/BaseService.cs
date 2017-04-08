using HD.Infrastructure.UnitOfWork;
using HD.Service.Interface;

namespace HD.Service.Implementation
{
    public class BaseService : IBaseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BaseService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public void BeginTran()
        {
            _unitOfWork.BeginTran();
        }

        public void CommitChange()
        {
            _unitOfWork.CommitChange();
        }

        public void CommitTran()
        {
            _unitOfWork.CommitTran();
        }

        public void RollbackTran()
        {
            _unitOfWork.RollbackTran();
        }
    }
}