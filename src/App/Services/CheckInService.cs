using System;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts;
using App.Entities;
using App.Exceptions.CheckInExceptions;
using Moq;

namespace App.Services
{
    public class CheckInService : ICheckInService
    {
        private readonly IRepository<Queue, int> _queueRepository;
        private readonly IRepository<Site, int> _siteRepository;
        private readonly INotificationService _notificationService;

        public CheckInService(IRepository<Queue, int> queueRepository, IRepository<Site, int> siteRepository)
        {
            _queueRepository = queueRepository;
            _siteRepository = siteRepository;
            _notificationService = new Mock<INotificationService>().Object;
        }

        public async Task<CheckInResult> CheckInAsync(int siteId, Customer customer)
        {
            var result = new CheckInResult();

            var siteResult = _siteRepository.FindByCondition(row => row.Id == siteId);

            if (!siteResult.Any())
            {
                // Specified site do not exist
                throw new SiteDoesNotExistException();
            }
            else
            {
                customer.Site = siteResult.First();
            }

            var checkInResult = _queueRepository.FindByCondition(row => row.IsCompleted == false 
            && row.SiteId == siteId 
            && row.CustomerId.Equals(customer.Id));

            if (checkInResult.Any())
            {
                // Customer already performed checkIn
                result.IsExistent = true;
                result.TicketNumber = checkInResult.First().TicketNumber;
            }
            else
            {
                var chekin = new Queue()
                {
                    Customer = customer,
                    SiteId = siteId,
                    TicketNumber = Guid.NewGuid(),
                    DateCreated = DateTime.UtcNow
                };

                await _queueRepository.Create(chekin);

                await _notificationService.NotifyCustomer(customer.PhoneNumber, chekin.TicketNumber);

                result.TicketNumber = chekin.TicketNumber;
            }

            return result;
        }
    }
}
