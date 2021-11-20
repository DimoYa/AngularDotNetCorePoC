namespace MyEdo.Business.Test.Common
{
    using Microsoft.AspNetCore.Identity;
    using MyEdo.Core.Common;
    using MyEdo.Core.Models;
    using MyEdo.Core.Models.Enums;
    using System;
    using System.Collections.Generic;

    public static class DummyData
    {
        public static List<UserRole> GetDummyUserRoles()
        {
            return new List<UserRole>()
            {
                new UserRole(GlobalConstants.ResourceRoleName)
                {
                    Id = "1"
                },
                new UserRole(GlobalConstants.AdministratorRoleName)
                {
                    Id = "2"
                }
            };
        }
        public static List<User> GetDummyUsers()
        {
            return new List<User>()
            {
                new User
                {
                    Id = "123",
                    FirstName = "FirstName",
                    LastName = "LastName",
                    IsDeleted = false,
                    LockoutEnabled = true,
                    Roles = new List<IdentityUserRole<string>>()
                    {
                        new  IdentityUserRole<string> { RoleId = "1", UserId = "123" },
                        new  IdentityUserRole<string> { RoleId = "2", UserId = "123" }
                    }
                },
                new User
                {
                    Id = "124",
                    FirstName = "Test",
                    LastName = "Test",
                    IsDeleted = false,
                    Roles = new List<IdentityUserRole<string>>()
                    {
                        new  IdentityUserRole<string> { RoleId = "1", UserId = "124" }
                    }
                }
            };
        }

        public static List<Skill> GetDummySkills()
        {
            return new List<Skill>()
            {
                new Skill()
                {
                    Id = "1",
                    Name = "Skill1",
                    IsDeleted = false,
                    SkillCategoryId = "10",
                },
                new Skill()
                {
                    Id = "2",
                    Name = "Skill2",
                    IsDeleted = false,
                    SkillCategoryId = "10",
                },
                new Skill()
                {
                    Id = "3",
                    Name = "Skill3",
                    IsDeleted = false,
                    SkillCategoryId = "11",
                },
                new Skill()
                {
                    Id = "4",
                    Name = "Skill4",
                    IsDeleted = true,
                    SkillCategoryId = "11",
                },
                new Skill()
                {
                    Id = "5",
                    Name = "Skill5",
                    IsDeleted = true,
                    SkillCategoryId = "13",
                }
            };
        }

        public static List<SkillCategory> GetDummySkillCategories()
        {
            return new List<SkillCategory>()
            {
                new SkillCategory()
                {
                    Id = "10",
                    Name = "Category1",
                    IsDeleted = false,
                },
                new SkillCategory()
                {
                    Id = "11",
                    Name = "Category2",
                    IsDeleted = false,
                },
                new SkillCategory()
                {
                    Id = "12",
                    Name = "Category3",
                    IsDeleted = true,
                },
                new SkillCategory()
                {
                    Id = "13",
                    Name = "Category4",
                    IsDeleted = true,
                }
            };
        }

        public static List<UserSkill> GetDummyUserSkills()
        {
            return new List<UserSkill>()
            {
                new UserSkill()
                {
                   UserId = "123",
                   SkillId = "1",
                },
               new UserSkill()
               {
                   UserId = "123",
                   SkillId = "2",
                   Level = SkillLevel.Beginning,
               },
               new UserSkill()
              {
                   UserId = "123",
                   SkillId = "4",
               },

                new UserSkill()
                {
                    UserId = "124",
                    SkillId = "1",
                },
                new UserSkill()
                {
                    UserId = "124",
                    SkillId = "2",
                },
                new UserSkill()
                {
                    UserId = "125",
                    SkillId = "1",
                },
                new UserSkill()
                {
                    UserId = "125",
                    SkillId = "3",
                },
            };
        }

        public static List<Training> GetDummyTrainings()
        {
            return new List<Training>()
            {
                new Training()
                {
                    Id = "1",
                    Name = "Training1",
                    IsDeleted = false,
                    DueDate = DateTime.Now.AddDays(30),
                },
                new Training()
                {
                    Id = "2",
                    Name = "Training2",
                    IsDeleted = false,
                    DueDate = DateTime.Now.AddDays(-1),
                },
                new Training()
                {
                    Id = "3",
                    Name = "Training3",
                    IsDeleted = false,
                    DueDate = DateTime.Now.AddDays(10),
                },
                new Training()
                {
                    Id = "4",
                    Name = "Training4",
                    IsDeleted = true,
                    DueDate = DateTime.Now.AddDays(5),
                },

            };
        }

        public static List<UserTraining> GetDummyUserTrainings()
        {
            return new List<UserTraining>()
            {
                new UserTraining()
                {
                   UserId = "123",
                   TrainingId = "1",
                },
                new UserTraining()
                {
                   UserId = "123",
                   TrainingId = "4",
                },
                new UserTraining()
                {
                   UserId = "123",
                   TrainingId = "3",
                },
                new UserTraining()
                {
                   UserId = "124",
                   TrainingId = "4",
                },
                new UserTraining()
                {
                   UserId = "128",
                   TrainingId = "1",
                   Status = UserTrainingStatus.Assigned,
                },
            };
        }
    }
}

