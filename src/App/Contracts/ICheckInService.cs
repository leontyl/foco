using System.Threading.Tasks;
using App.Entities;
using App.Services;

namespace App.Contracts
{
    public interface ICheckInService
    {
        public Task<CheckInResult> CheckInAsync(int siteId, Customer customer);
    }
}
