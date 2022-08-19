using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using App.Contracts;

namespace App.Data
{
    public abstract class RepositoryBase<T, TK> : IRepository<T, TK> where T : class
    {
        private readonly FoCoDbContext _dbContext;

        public RepositoryBase(FoCoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual IQueryable<T> FindAll()
        {
            var result = _dbContext.Set<T>().AsQueryable();
            return result;
        }

        public virtual IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            var result = _dbContext.Set<T>().Where(expression);
            return result;
        }

        public virtual async Task Create(T entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task  Update(T entity, TK id)
        {
            var current = _dbContext.Find<T>(id);
            _dbContext.Entry(current).CurrentValues.SetValues(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
