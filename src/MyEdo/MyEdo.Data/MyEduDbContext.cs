using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyEdo.Core.Models;

namespace MyEdo.Data
{
    public class MyEduDbContext : KeyApiAuthorizationDbContext<User, UserRole, string>
    {
        public MyEduDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ConfigureUserIdentityRelations(builder);

            builder.Entity<UserSkill>()
                   .HasKey(us => new
                   {
                       us.UserId,
                       us.SkillId,
                   });

            builder.Entity<UserTraining>()
                  .HasKey(ut => new
                  {
                      ut.UserId,
                      ut.TrainingId,
                  });
        }

        private static void ConfigureUserIdentityRelations(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasMany(e => e.Roles)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<Skill> Skills { get; set; }

        public DbSet<SkillCategory> SkillCategories { get; set; }

        public DbSet<Training> Trainings { get; set; }

        public DbSet<UserSkill> UserSkills { get; set; }

        public DbSet<UserTraining> UserTrainings { get; set; }
    }
}
