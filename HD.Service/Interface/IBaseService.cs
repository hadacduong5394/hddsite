namespace HD.Service.Interface
{
    public interface IBaseService
    {
        void CommitChange();

        void BeginTran();

        void CommitTran();

        void RollbackTran();
    }
}