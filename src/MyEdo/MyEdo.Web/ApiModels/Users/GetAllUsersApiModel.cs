using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using MyEdo.Core.Models;
using System.Linq;

namespace MyEdo.Web.ApiModels.Users
{
    public class GetAllUsersApiModel
    {
        public GetAllUsersApiModel(User user)
        {
            Id = user.Id;
            UserName = user.UserName;
            FullName = $"{user.FirstName} {user.LastName}";
            IsLocked = user.LockoutEnd != null;
            Roles = user.Roles.Select(x => new UserRoleApiModel() { Id = x.RoleId}).ToList();
        }
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public bool IsLocked { get; set; }

        public IEnumerable<UserRoleApiModel> Roles { get; set; }
    }
}
