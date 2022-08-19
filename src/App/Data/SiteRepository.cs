using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using App.Contracts;
using App.Entities;

namespace App.Data
{
    public class SiteRepository : IRepository<Site>
    {
        private readonly FoCoDbContext _dbContext;

        public SiteRepository(FoCoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Site> FindAll()
        {
            var result =  _dbContext.Sites.AsQueryable();
            return result;
        }

        public IQueryable<Site> FindByCondition(Expression<Func<Site, bool>> expression)
        {
            var result = _dbContext.Sites.Where(expression);
            return result;
        }

        public async Task Create(Site entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
