using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace App.Contracts
{
    public interface IRepository<T, in TK> where T : class
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        Task Create(T entity);
        Task Update(T entity, TK id);
    }
}
