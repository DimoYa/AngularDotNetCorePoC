using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyEdo.Business.Services.AppUser;
using MyEdo.Core.Models;
using MyEdo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEdo.Business.Services.AppAdmin
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

        public Task<IEnumerable<User>> GetAllActiveUsers()
        {
            var activeUsers = this.context.Users
                .Include(u => u.Roles)
                .Where(u => u.IsDeleted == false)
                .OrderBy(u => u.FirstName)
                .ThenBy(u => u.LastName)
                .ToList();

            return Task.FromResult(activeUsers.AsEnumerable());
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
    }
}
