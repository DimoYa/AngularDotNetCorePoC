using Microsoft.AspNetCore.Http;
using MyEdo.Core.Common;
using MyEdo.Core.Models;
using MyEdo.Data;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyEdo.Business.Services.AppUser
{
    public class UserService : IUserService
    {
        private readonly MyEduDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserService(
            MyEduDbContext context,
            IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<User>> GetAllActiveResources<TViewModel>()
        {
            var resourceRoleId = await this.GetRoleIdByName(GlobalConstants.ResourceRoleName);

            var userWithSkills = context.Users
                .Where(u => u.IsDeleted == false && u.Roles.Any(r => r.RoleId == resourceRoleId))
                .OrderBy(u => u.FirstName)
                .ThenBy(u => u.LastName)
                .ToList();

            return userWithSkills;
        }

        public Task<string> GetCurrentUserId()
        {
            var currentUser = httpContextAccessor
                .HttpContext
                .User
                .FindFirst(ClaimTypes.NameIdentifier)
                .Value;

            return Task.FromResult(currentUser);
        }

        public Task<string> GetCurrentUserName()
        {
            var currentUser = httpContextAccessor
                .HttpContext
                .User
                .FindFirst(ClaimTypes.Name)
                .Value;

            return Task.FromResult(currentUser);
        }

        public Task<User> GetUserByName(string firstName, string lastName)
        {
            var user = context.Users
                .SingleOrDefault(u => u.FirstName == firstName && u.LastName == lastName);

            return Task.FromResult(user);
        }

        public Task<User> GetUserById(string id)
        {
            var user = context.Users
                .SingleOrDefault(u => u.Id == id);

            return Task.FromResult(user);
        }

        public Task<string> GetRoleIdByName(string roleName)
        {
            var resourceRoleId = context.Roles
                .FirstOrDefault(x => x.Name == roleName)
                .Id;

            return Task.FromResult(resourceRoleId);
        }

        public Task<string> GetRoleNameById(string roleId)
        {
            var resourceRoleName = context.Roles
                .FirstOrDefault(r => r.Id == roleId)
                .Name;

            return Task.FromResult(resourceRoleName);
        }
    }
}