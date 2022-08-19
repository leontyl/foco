using System.Linq;
using System.Threading.Tasks;
using App.Contracts;
using App.Entities;
using App.Exceptions.CheckInExceptions;

namespace App.Services
{
    public class ActionService : IActionService
    {
        private readonly IRepository<Queue, int> _queueRepository;
        private readonly IRepository<Site, int> _siteRepository;
        private readonly IRepository<Customer, string> _customerRepository;

        public ActionService(IRepository<Queue, int> queueRepository, IRepository<Site, int> siteRepository, IRepository<Customer, string> customerRepository)
        {
            _queueRepository = queueRepository;
            _siteRepository = siteRepository;
            _customerRepository = customerRepository;
        }

        public async Task<Queue> GetNext(int siteId)
        {
            var site = _siteRepository.FindByCondition(row => row.Id == siteId);

            if (!site.Any())
            {
                throw new SiteDoesNotExistException();
            }

            var queueResult = _queueRepository.FindByCondition(row => row.SiteId == siteId && row.IsCompleted == false);

            if (!queueResult.Any())
            {
                return null;
            }

            var queue = queueResult.OrderByDescending(k => k.DateCreated)
                .Take(1).First();

            queue.IsCompleted = true;
            await _queueRepository.Update(queue, queue.Id);

            queue.Customer = _customerRepository.FindByCondition(row => row.Id.Equals(queue.CustomerId))
                .FirstOrDefault();

            return queue;
        }
    }
}
