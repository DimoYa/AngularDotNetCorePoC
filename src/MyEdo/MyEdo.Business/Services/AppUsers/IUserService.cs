using MyEdo.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyEdo.Business.Services.AppUser
{
    public interface IUserService
    {
        Task<string> GetRoleNameById(string roleId);

        Task<string> GetCurrentUserId();

        Task<User> GetUserById(string id);
    }
}
