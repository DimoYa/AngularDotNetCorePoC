using MyEdo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEdo.Services.AppUser
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
