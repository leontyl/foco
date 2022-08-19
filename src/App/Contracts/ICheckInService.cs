using System;
using System.Threading.Tasks;
using App.Entities;

namespace App.Contracts
{
    public interface ICheckInService
    {
        public Task<Guid> CheckInAsync(int siteId, Customer customer);
    }
}
