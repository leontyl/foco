using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using App.Contracts;
using App.Entities;

namespace App.Data
{
    public class QueueRepository : IRepository<Queue>
    {
        private readonly FoCoDbContext _dbContext;

        public QueueRepository(FoCoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Queue> FindAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Queue> FindByCondition(Expression<Func<Queue, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task Create(Queue entity)
        {
            await _dbContext.Queues.AddAsync(entity);
        }
    }
}
