using App.Entities;

namespace App.Data
{
    public class QueueRepository : RepositoryBase<Queue, int>
    {
        public QueueRepository(FoCoDbContext dbContext) : base(dbContext)
        {
        }
    }
}
