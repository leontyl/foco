using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using App.Contracts;
using App.Entities;

namespace App.Data
{
    public class SiteRepository : RepositoryBase<Site, int>
    {
        public SiteRepository(FoCoDbContext dbContext) : base(dbContext)
        {
        }
    }
}
