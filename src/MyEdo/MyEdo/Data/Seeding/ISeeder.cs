using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEdo.Data.Seeding
{
    interface ISeeder
    {
        Task SeedAsync(MyEduDbContext dbContext, IServiceProvider serviceProvider);
    }
}