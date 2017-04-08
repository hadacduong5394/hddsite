namespace HD.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        void CommitChange();

        void BeginTran();

        void CommitTran();

        void RollbackTran();
    }
}