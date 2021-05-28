using MyEdo.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyEdo.Business.Services.Admin
{
    public interface IAdminService
    {
        Task<IEnumerable<User>> GetAllActiveUsers<TViewModel>();

        Task<bool> Lock(string id);

        Task<bool> Unlock(string id);
    }
}
