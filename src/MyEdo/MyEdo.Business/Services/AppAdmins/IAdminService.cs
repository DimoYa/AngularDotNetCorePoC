using MyEdo.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyEdo.Business.Services.AppAdmin
{
    public interface IAdminService
    {
        Task<IEnumerable<User>> GetAllActiveUsers();

        Task<bool> Lock(string id);

        Task<bool> Unlock(string id);
    }
}
