using System;
using System.Linq;
using System.Linq.Expressions;

namespace HD.Infrastructure.Repository
{
    public interface IRepositoryBase<T, TId> where T : class
    {
        // Marks an entity as new
        T Add(T entity);

        void CreateNew(T entity);

        // Marks an entity as modified
        void Update(T entity);

        // Marks an entity to be removed by id
        void Delete(TId id);

        //Delete multi records
        void DeleteMulti(Expression<Func<T, bool>> where);

        // Get an entity by int id
        T GetSingleById(TId id);

        T GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null);

        IQueryable<T> GetAll(string[] includes = null);

        IQueryable<T> GetMulti(Expression<Func<T, bool>> predicate, string[] includes = null);

        IQueryable<T> GetMultiPaging(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50, string[] includes = null);

        int Count(Expression<Func<T, bool>> where);

        bool CheckContains(Expression<Func<T, bool>> predicate);
    }
}