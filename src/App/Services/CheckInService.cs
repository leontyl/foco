using System;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts;
using App.Entities;
using App.Exceptions.CheckInExceptions;

namespace App.Services
{
    public class CheckInService : ICheckInService
    {
        private IRepository<Queue> _queueRepository;
        private IRepository<Site> _siteRepository;

        public CheckInService(IRepository<Site> siteRepository, IRepository<Queue> queueRepository)
        {
            _siteRepository = siteRepository;
            _queueRepository = queueRepository;
        }

        public async Task<Guid> CheckInAsync(int siteId, Customer customer)
        {
            var siteResult = _siteRepository.FindByCondition(site => site.Id == siteId);
            var siteFirstOrDefault = siteResult.FirstOrDefault();

            if (siteFirstOrDefault == null)
            {
                throw new SiteDoesNotExistException();
            }

            var queue = new Queue()
            {
                Site = siteFirstOrDefault,
                Customer = customer,
                TicketNumber = Guid.NewGuid()
            };

            await _queueRepository.Create(queue);

            return queue.TicketNumber;
        }
    }
}
