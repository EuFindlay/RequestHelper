using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RequestHelperSample.Data.Repositories.Base
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        Task AddAsync(T entity);
        void Update(T entity);
        void Update(IEnumerable<T> entities);
        void Delete(T entity);
        void Delete(int id);
        T GetById(int Id);
        T GetById(Guid Id);
        Task<T> GetByIdAsync(int Id);
        Task<T> GetByIdAsync(Guid Id);

        T Get(Expression<Func<T, bool>> where);
        Task<T> GetAsync(Expression<Func<T, bool>> where);

        List<T> GetMany(Expression<Func<T, bool>> where);
        IQueryable<T> GetManyQ(Expression<Func<T, bool>> where);
        Task<List<T>> GetManyAsync(Expression<Func<T, bool>> where);
        Task<List<T>> GetManyIncludingAllAsync(Expression<Func<T, bool>> where);

        List<T> GetAll();
        IQueryable<T> GetAllQ();
        Task<List<T>> GetAllAsync();
        IQueryable<T> GetAllIncludingAll();

        IQueryable<T> Table { get; }
        IQueryable<T> GetAllIncludingProperties(params string[] properties);
        T GetIncludingAll(Expression<Func<T, bool>> where);
        Task<T> GetIncludingAllAsync(Expression<Func<T, bool>> where);
        IQueryable<T> GetQueryWithInclude(params string[] include);
        IQueryable<T> GetManyIncludingProperties(Expression<Func<T, bool>> where, params string[] properties);
        Task<int> CountAsync(Expression<Func<T, bool>> where);
        T GetWithInclude(Expression<Func<T, bool>> where, params string[] include);
        void Commit();
        Task CommitAsync();
    }
}
