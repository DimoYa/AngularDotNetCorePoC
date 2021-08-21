using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MyEdo.Business.Exceptions;
using MyEdo.Core.Common;
using MyEdo.Core.Models;
using MyEdo.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEdo.Business.Services.AppUser
{
    public class UserService : IUserService
    {
        private readonly MyEduDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;

        public UserService(
            MyEduDbContext context,
            IHttpContextAccessor httpContextAccessor,
            UserManager<User> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }


        public async Task<string> GetCurrentUserId()
        {
            var userName = await this.GetCurrentUserName();

            var currentUserId = this.context
                .Users
                .FirstOrDefault(x => x.Email == userName)
                .Id;

            if (currentUserId == null)
            {
                throw new NotFoundException();
            }

            return currentUserId;
        }

        public Task<User> GetUserById(string id)
        {
            var user = context.Users
                .SingleOrDefault(u => u.Id == id);

            if (user == null)
            {
                throw new NotFoundException();
            }

            return Task.FromResult(user);
        }

        public Task<string> GetRoleNameById(string roleId)
        {
            var resourceRoleName = context.Roles
                .FirstOrDefault(r => r.Id == roleId)
                .Name;

            return Task.FromResult(resourceRoleName);
        }

        public async Task<IEnumerable<string>> GetUserRoles()
        {
            var user = await this.userManager.FindByEmailAsync(await this.GetCurrentUserName());
            var userRoles = await userManager.GetRolesAsync(user);

            return userRoles;
        }

        private Task<string> GetCurrentUserName()
        {
            var currentUser = httpContextAccessor
                .HttpContext
                .User
                .Identities
                .FirstOrDefault()
                .Name;

            return Task.FromResult(currentUser);
        }
    }
}