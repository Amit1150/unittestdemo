using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MediaStorage.Data
{
    public interface IRepository<T> where T : BaseEntity
    {
        void Add(T entity);
        void AddRange(ICollection<T> entities);
        bool Any(Expression<Func<T, bool>> predicate);
        int Count();
        int Count(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        void Delete(object id);
        void Delete(T entity);
        void DeleteAll(Expression<Func<T, bool>> predicate);
        void DeleteRange(ICollection<T> entities);
        T Find(object key);
        Task<T> FindAsync(object key);
        T Get(Expression<Func<T, bool>> predicate);
        T Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        void Update(T entity);
    }
}