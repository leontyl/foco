using App.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace App.Data
{
    public class FoCoDbContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;

        public FoCoDbContext(DbContextOptions options, ILoggerFactory loggerFactory) : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory); // Logging db commands

            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Site> Sites { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Queue> Queues { get; set; }
    }
}
