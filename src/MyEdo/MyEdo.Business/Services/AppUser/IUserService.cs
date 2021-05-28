using MyEdo.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyEdo.Business.Services.AppUser
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllActiveResources<TViewModel>();

        Task<string> GetRoleIdByName(string roleName);

        Task<string> GetRoleNameById(string roleId);

        Task<string> GetCurrentUserId();

        Task<string> GetCurrentUserName();

        Task<User> GetUserByName(string firstName, string lastName);

        Task<User> GetUserById(string id);
    }
}
