using MyEdo.ApiModels;
using MyEdo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEdo.Services.Admin
{
    public interface IAdminService
    {
        Task<IEnumerable<User>> GetAllActiveUsers<TViewModel>();

        Task<bool> Lock(string id);

        Task<bool> Unlock(string id);

        Task ManageUserRoles(string id, AdminManageUserRolesApiModel model);

        Task<AdminManageUserRolesApiModel> GetUserRolesById(string id);
    }
}
