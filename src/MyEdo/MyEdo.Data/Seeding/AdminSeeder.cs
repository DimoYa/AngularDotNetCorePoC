using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MyEdo.Core.Common;
using MyEdo.Core.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyEdo.Data.Seeding
{
    internal class AdminSeeder : ISeeder
    {
        public async Task SeedAsync(MyEdoDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<UserRole>>();

            var adminUser = new User()
            {
                UserName = "admin@admin.bg",
                Email = "admin@admin.bg",
                FirstName = "Admin",
                LastName = "Admin",
            };

            await SeedAdminAsync(userManager, roleManager, adminUser);
        }

        private static async Task SeedAdminAsync(UserManager<User> userManager, RoleManager<UserRole> roleManager, User admin)
        {
            User user = await GetAdminUser(userManager, admin.Email);
            if (user == null)
            {
                var result = await userManager.CreateAsync(admin, "admin");
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }

                var adminRoleName = GlobalConstants.AdministratorRoleName;

                var role = await roleManager.FindByNameAsync(adminRoleName);

                if (role != null)
                {
                    User adminUser = await GetAdminUser(userManager, admin.Email);
                    var addRoleResult = await userManager.AddToRoleAsync(adminUser, adminRoleName);

                    if (!addRoleResult.Succeeded)
                    {
                        throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                    }
                }
            }
        }

        private static async Task<User> GetAdminUser(UserManager<User> userManager, string email)
        {
            return await userManager.FindByEmailAsync(email);
        }
    }
}