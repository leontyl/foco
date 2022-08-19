using System;
using System.Threading.Tasks;

namespace App.Contracts
{
    public interface INotificationService
    {
        public Task NotifyCustomer(string customerPhoneNumber, Guid ticketNumber);
    }
}
