using HD.Context;

namespace HD.Infrastructure.DBFactory
{
    public class DbFactory : Disposable, IDbFactory
    {
        private ContextConnection dbContext;

        public ContextConnection Init()
        {
            return dbContext ?? (dbContext = new ContextConnection());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}