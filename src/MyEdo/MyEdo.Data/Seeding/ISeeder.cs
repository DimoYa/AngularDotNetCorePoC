using System;
using System.Threading.Tasks;

namespace MyEdo.Data.Seeding
{
    interface ISeeder
    {
        Task SeedAsync(MyEduDbContext dbContext, IServiceProvider serviceProvider);
    }
}