using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyEdo.ApiModels;
using MyEdo.Common;
using MyEdo.Data;
using MyEdo.Models;
using MyEdo.Services.AppUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEdo.Services.Admin
{
    public class AdminService : IAdminService
    {
        private readonly MyEduDbContext context;
        private readonly IUserService userService;
        private readonly UserManager<User> userManager;

        public AdminService(
            MyEduDbContext context,
            IUserService userService,
            UserManager<User> userManager)
        {
            this.context = context;
            this.userService = userService;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<User>> GetAllActiveUsers<TViewModel>()
        {
            var activeUsers = this.context.Users
                .Include(u => u.Roles)
                .Where(u => u.IsDeleted == false)
                .OrderBy(u => u.FirstName)
                .ThenBy(u => u.LastName);

            foreach (var user in activeUsers)
            {
                foreach (var role in user.Roles)
                {
                    var roleName = await this.userService.GetRoleNameById(role.RoleId);
                    role.RoleId = roleName;
                }
            }

            return activeUsers.ToList();
        }

        public async Task<bool> Lock(string id)
        {
            var userToLock = await this.userService.GetUserById(id);

            await this.userManager.SetLockoutEndDateAsync(userToLock, DateTime.Now.AddHours(1));

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> Unlock(string id)
        {
            var userToUnLock = await this.userService.GetUserById(id);

            await this.userManager.SetLockoutEndDateAsync(userToUnLock, null);

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task ManageUserRoles(string id, AdminManageUserRolesApiModel model)
        {
            var userToUpdate = await this.userService.GetUserById(id);

            var currentUserRoles = this.context.UserRoles
                .Where(u => u.UserId == id);

            this.context.UserRoles.RemoveRange(currentUserRoles);

            await this.context.SaveChangesAsync();

            if (model.Resource == true)
            {
                await this.userManager.AddToRoleAsync(userToUpdate, GlobalConstants.ResourceRoleName);
            }

            if (model.Admin == true)
            {
                await this.userManager.AddToRoleAsync(userToUpdate, GlobalConstants.AdministratorRoleName);
            }
        }

        public async Task<AdminManageUserRolesApiModel> GetUserRolesById(string userId)
        {
            var user = this.context.Users
               .Include(u => u.Roles)
               .Where(u => u.IsDeleted == false)
               .Where(u => u.Id == userId)
               .SingleOrDefault();

            var model = new AdminManageUserRolesApiModel();

            model.FullName = $"{user.FirstName} {user.LastName}";

            foreach (var role in user.Roles)
            {
                var roleName = await this.userService.GetRoleNameById(role.RoleId);

                switch (roleName)
                {
                    case GlobalConstants.ResourceRoleName:
                        model.Resource = true;
                        break;
                    case GlobalConstants.AdministratorRoleName:
                        model.Admin = true;
                        break;
                }
            }

            return model;
        }
    }
}
