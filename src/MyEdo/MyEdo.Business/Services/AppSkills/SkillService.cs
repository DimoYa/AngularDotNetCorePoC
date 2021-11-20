using Microsoft.EntityFrameworkCore;
using MyEdo.Business.Exceptions;
using MyEdo.Business.Services.AppSkillCategory;
using MyEdo.Business.Services.AppUser;
using MyEdo.Core.Models;
using MyEdo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEdo.Business.Services.AppSkill
{
    public class SkillService : ISkillService
    {
        private readonly MyEdoDbContext context;
        private readonly ISkillCategoryService skillCategoryService;
        private readonly IUserService userService;

        public SkillService(
            MyEdoDbContext context,
            ISkillCategoryService skillCategoryService,
            IUserService userService)
        {
            this.context = context;
            this.skillCategoryService = skillCategoryService;
            this.userService = userService;
        }

        public async Task<string> CreateSkill(Skill model)
        {
            var skillCategory = await this.skillCategoryService.GetCategoryById(model.SkillCategory.Id);

            Skill skill = new Skill
            {
                Name = model.Name,
                CreatedOn = DateTime.UtcNow,
                SkillCategory = skillCategory,
            };

            this.context.Skills.Add(skill);
            await this.context.SaveChangesAsync();

            return skill.Id;
        }

        public async Task<bool> EditSkill(Skill model)
        {
            var skillForUpdate = await this.GetSkillById(model.Id);

            var skillCategory = await this.skillCategoryService.GetCategoryById(model.SkillCategoryId);

            skillForUpdate.Name = model.Name;
            skillForUpdate.SkillCategory = skillCategory;
            skillForUpdate.ModifiedOn = DateTime.UtcNow;

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> EditSkillLevel(UserSkill model)
        {
            var skillForUpdate = await this.GetCurrentuserSkillById(model.SkillId);

            skillForUpdate.Level = model.Level;

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteSkill(string skillId)
        {
            var skillForDeletion = await this.GetSkillById(skillId);

            skillForDeletion.IsDeleted = true;
            skillForDeletion.DeletedOn = DateTime.UtcNow;

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> AddSkillToMyProfile(UserSkill model)
        {
            var currentUserId = await this.userService.GetCurrentUserId();

            UserSkill userSkill = new UserSkill
            {
                UserId = currentUserId,
                SkillId = model.SkillId,
                Level = model.Level
            };

            this.context.UserSkills.Add(userSkill);
            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> RemoveSkillFromProfile(string id)
        {
            var skillToRemove = await this.GetCurrentuserSkillById(id);

            this.context.UserSkills.Remove(skillToRemove);
            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public Task<IEnumerable<SkillCategory>> GetAllSkillsByCategories()
        {
            var skillsByCategories = this.context.SkillCategories
                .Where(s => s.IsDeleted == false)
                .Include(s => s.Skills)
                .ToList();

            foreach (var category in skillsByCategories)
            {
                foreach (var skill in category.Skills)
                {
                    if (skill.IsDeleted == true)
                    {
                        skillsByCategories.ForEach(c => c.Skills.Remove(skill));
                    }
                }
            }

            return Task.FromResult(skillsByCategories.AsEnumerable());
        }

        public Task<IEnumerable<UserSkill>> GetAllUsersSkills()
        {

            var userSkillsByCategories = this.context.UserSkills
                 .Include(s => s.Skill).ThenInclude(c => c.SkillCategory)
                 .Include(s => s.User)
                 .Where(s => s.Skill.IsDeleted == false)
                 .ToList();

            return Task.FromResult(userSkillsByCategories.AsEnumerable());
        }

        public Task<IEnumerable<UserSkill>> GetMySkills()
        {
            var currentUserId = this.userService.GetCurrentUserId();

            var skillsByCategories = this.context.UserSkills
                 .Include(s => s.Skill).ThenInclude(c => c.SkillCategory)
                 .Where(us => us.UserId == currentUserId.Result)
                 .Where(s => s.Skill.IsDeleted == false)
                 .ToList();

            return Task.FromResult(skillsByCategories.AsEnumerable());
        }

        public Task<Skill> GetSkillById(string id)
        {
            var skill = this.context.Skills
                  .Include(s => s.SkillCategory)
                  .Where(s => s.Id == id)
                  .SingleOrDefault();

            if (skill == null)
            {
                throw new NotFoundException();
            }

            return Task.FromResult(skill);
        }

        private async Task<UserSkill> GetCurrentuserSkillById(string skillId)
        {
            var currentUser = await this.userService.GetCurrentUserId();

            var skill = this.context.UserSkills
                  .Where(s => s.SkillId == skillId && s.UserId == currentUser)
                  .SingleOrDefault();

            if (skill == null)
            {
                throw new NotFoundException();
            }

            return skill;
        }
    }
}
