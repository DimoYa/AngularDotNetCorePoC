using Microsoft.EntityFrameworkCore;
using MyEdo.Business.Services.AppSkillCategory;
using MyEdo.Business.Services.AppUser;
using MyEdo.Core.Models;
using MyEdo.Core.Models.Enums;
using MyEdo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEdo.Business.Services.AppSkill
{
    public class SkillService : ISkillService
    {
        private readonly MyEduDbContext context;
        private readonly ISkillCategoryService skillCategoryService;
        private readonly IUserService userService;

        public SkillService(
            MyEduDbContext context,
            ISkillCategoryService skillCategoryService,
            IUserService userService)
        {
            this.context = context;
            this.skillCategoryService = skillCategoryService;
            this.userService = userService;
        }

        public async Task<string> CreateSkill(Skill model)
        {
            var skillCategory = await this.skillCategoryService.GetCategoryById(model.SkillCategoryId);

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

        public async Task<bool> EditSkill(Skill model, string id)
        {
            var skillForUpdate = await this.GetSkillById(id);

            var skillCategory = await this.skillCategoryService.GetCategoryById(model.SkillCategoryId);

            skillForUpdate.Name = model.Name;
            skillForUpdate.SkillCategory = skillCategory;
            skillForUpdate.ModifiedOn = DateTime.UtcNow;

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> EditSkillLevel(UserSkill model, string id)
        {
            var skillForUpdate = await this.GetCurrentuserSkillById(id);

            skillForUpdate.Level = model.Level;

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteSkill(string id)
        {
            var skillForDeletion = await this.GetSkillById(id);

            skillForDeletion.IsDeleted = true;
            skillForDeletion.DeletedOn = DateTime.UtcNow;

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> AddSkillToMyProfile(string id, int level)
        {
            var currentUserId = await this.userService.GetCurrentUserId();

            UserSkill userSkill = new UserSkill
            {
                UserId = currentUserId,
                SkillId = id,
                Level = (SkillLevel)level
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

        public Task<IEnumerable<UserSkill>> GetAllSkillsByCategories()
        {
            var skillsByCategories = this.context.UserSkills
                .Include(us => us.Skill).ThenInclude(c => c.SkillCategory)
                .Where(s => s.Skill.IsDeleted == false)
                .ToList();

            return Task.FromResult(skillsByCategories.AsEnumerable());
        }

        public async Task<IEnumerable<UserSkill>> GetUserSkillsByCategories()
        {
            var currentUserId = await this.userService.GetCurrentUserId();

            var skillsByCategories = this.context.UserSkills
                 .Include(us => us.Skill).ThenInclude(c => c.SkillCategory)
                 .Where(us => us.UserId == currentUserId)
                 .Where(s => s.Skill.IsDeleted == false)
                 .ToList();

            return skillsByCategories;
        }

        public Task<Skill> GetSkillById(string id)
        {
            var skill = this.context.Skills
                  .Where(s => s.Id == id)
                  .SingleOrDefault();

            var skillCategory = this.context
                .SkillCategories
                .SingleOrDefault(s => s.Id == skill.SkillCategoryId);

            skill.SkillCategory = skillCategory;

            return Task.FromResult(skill);
        }

        public async Task<UserSkill> GetCurrentuserSkillById(string skillId)
        {
            var currentUser = await this.userService.GetCurrentUserId();

            var skill = this.context.UserSkills
                  .Where(s => s.SkillId == skillId && s.UserId == currentUser)
                  .SingleOrDefault();

            return skill;
        }

        public async Task<IList<string>> GetCurrentUserSkillsId()
        {
            var currentUserId = await this.userService.GetCurrentUserId();

            var userSkillsId = this.context.UserSkills
                .Where(u => u.UserId == currentUserId)
                .Select(s => s.SkillId)
                .ToList();

            return userSkillsId;
        }
    }
}
