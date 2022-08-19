using System.Threading.Tasks;
using App.Entities;

namespace App.Contracts
{
    public interface IActionService
    {
        public Task<Queue> GetNext(int siteId);
    }
}
