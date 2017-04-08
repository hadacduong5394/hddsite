using HD.Context;
using System;

namespace HD.Infrastructure.DBFactory
{
    public interface IDbFactory : IDisposable
    {
        ContextConnection Init();
    }
}