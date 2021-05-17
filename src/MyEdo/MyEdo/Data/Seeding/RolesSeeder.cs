using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MyEdo.Common;
using MyEdo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEdo.Data.Seeding
{
    internal class RolesSeeder : ISeeder
    {
        public async Task SeedAsync(MyEduDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<UserRole>>();

            await SeedRoleAsync(roleManager, GlobalConstants.AdministratorRoleName);
            await SeedRoleAsync(roleManager, GlobalConstants.ResourceRoleName);
        }

        private static async Task SeedRoleAsync(RoleManager<UserRole> roleManager, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                var result = await roleManager.CreateAsync(new UserRole(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}