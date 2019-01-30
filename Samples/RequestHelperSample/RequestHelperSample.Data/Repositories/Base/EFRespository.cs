using Microsoft.EntityFrameworkCore;
using RequestHelperSample.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RequestHelperSample.Data.Repositories.Base
{
    public class EFRespository<T> where T : class
    {
        protected readonly DbSet<T> dbset;
        protected readonly DatabaseContext _dbContext;

        public EFRespository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
            dbset = dbContext.Set<T>();
        }

        public virtual void Add(T entity)
        {
            dbset.Add(entity);
        }

        public async Task AddAsync(T entity)
        {
            await dbset.AddAsync(entity);
        }

        public virtual void Update(T entity)
        {
            dbset.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
        public virtual void Update(IEnumerable<T> entities)
        {
            dbset.AttachRange(entities);
            _dbContext.Entry(entities).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            dbset.Remove(entity);
        }

        public virtual void Delete(int id)
        {
            var entity = dbset.Find(id);
            if (entity != null)
            {
                dbset.Remove(entity);
            }
        }

        public virtual T GetIncludingAll(Expression<Func<T, bool>> where)
        {
            var query = dbset.AsQueryable();

            foreach (var property in _dbContext.Model.FindEntityType(typeof(T)).GetNavigations())
                query = query.Include(property.Name);

            return query.FirstOrDefault(where);
        }

        public virtual IQueryable<T> GetQueryWithInclude(params string[] include)
        {
            var query = dbset.AsQueryable();

            foreach (var property in include)
                query = query.Include(property);

            return query;
        }

        public virtual T GetWithInclude(Expression<Func<T, bool>> where, params string[] include)
        {
            var query = dbset.AsQueryable();

            foreach (var property in include)
                query = query.Include(property);

            return query.FirstOrDefault(where);
        }

        public async virtual Task<T> GetIncludingAllAsync(Expression<Func<T, bool>> where)
        {
            var query = dbset.AsQueryable();

            foreach (var property in _dbContext.Model.FindEntityType(typeof(T)).GetNavigations())
                query = query.Include(property.Name);

            return await query.FirstOrDefaultAsync(where);
        }

        public virtual T GetById(int id)
        {
            return dbset.Find(id);
        }

        public virtual T GetById(Guid id)
        {
            return dbset.Find(id);
        }

        public virtual List<T> GetAll()
        {
            return dbset.ToList();
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await dbset.ToListAsync();
        }

        public virtual IQueryable<T> GetAllIncludingAll()
        {
            var query = dbset.AsQueryable();

            foreach (var property in _dbContext.Model.FindEntityType(typeof(T)).GetNavigations())
                query = query.Include(property.Name);

            return query;
        }

        public virtual IQueryable<T> GetAllIncludingProperties(params string[] properties)
        {
            var query = dbset.AsQueryable();

            foreach (var property in properties)
                query = query.Include(property);

            return query;
        }

        public virtual IQueryable<T> GetManyIncludingProperties(Expression<Func<T, bool>> where, params string[] properties)
        {
            var query = dbset.Where(where);

            foreach (var property in properties)
                query = query.Include(property);

            return query;
        }

        public virtual IQueryable<T> GetAllQ()
        {
            return dbset;
        }

        public virtual List<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).ToList();
        }

        public virtual async Task<List<T>> GetManyAsync(Expression<Func<T, bool>> where)
        {
            return await dbset.Where(where).ToListAsync();
        }

        public virtual async Task<List<T>> GetManyIncludingAllAsync(Expression<Func<T, bool>> where)
        {
            var query = dbset.AsQueryable();

            foreach (var property in _dbContext.Model.FindEntityType(typeof(T)).GetNavigations())
                query = query.Include(property.Name);

            return await query.Where(where).ToListAsync();
        }

        public virtual IQueryable<T> GetManyQ(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where);
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return dbset.FirstOrDefault(where);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> where)
        {
            return await dbset.FirstOrDefaultAsync(where);
        }


        public IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = dbset;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public virtual IQueryable<T> Table
        {
            get
            {
                return dbset;
            }
        }

        public virtual async Task<T> GetByIdAsync(int Id)
        {
            return await dbset.FindAsync(Id);
        }

        public virtual async Task<T> GetByIdAsync(Guid Id)
        {
            return await dbset.FindAsync(Id);
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> where)
        {
            return await dbset.CountAsync(where);
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
