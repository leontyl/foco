using App.Entities;

namespace App.Data
{
    public class CustomerRepository : RepositoryBase<Customer, string>
    {
        public CustomerRepository(FoCoDbContext dbContext) : base(dbContext)
        {
        }
    }
}
