using MyEdo.Core.Models;
using MyEdo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEdo.Business.Services.AppSkillCategory
{
    public class SkillCategoryService : ISkillCategoryService
    {
        private readonly MyEduDbContext context;

        public SkillCategoryService(MyEduDbContext context)
        {
            this.context = context;
        }

        public async Task<string> CreateCategory(SkillCategory model)
        {
            SkillCategory skillCategory = new SkillCategory
            {
                Name = model.Name,
            };

            this.context.SkillCategories.Add(skillCategory);
            await this.context.SaveChangesAsync();

            return skillCategory.Id;
        }

        public async Task<bool> EditCategory(SkillCategory model)
        {
            var categoryForUpdate = await this.GetCategoryById(model.Id);

            categoryForUpdate.Name = model.Name;
            categoryForUpdate.ModifiedOn = DateTime.UtcNow;

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteCategory(string id)
        {
            var skillCategoryForDeletion = this.context.SkillCategories
                .SingleOrDefault(s => s.Id == id);

            skillCategoryForDeletion.IsDeleted = true;
            skillCategoryForDeletion.DeletedOn = DateTime.UtcNow;

            var skillsUnderTheCategory = this.context.Skills
                .Where(s => s.SkillCategoryId == skillCategoryForDeletion.Id);

            foreach (var skill in skillsUnderTheCategory)
            {
                skill.IsDeleted = true;
                skill.DeletedOn = DateTime.UtcNow;
            }

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public Task<IEnumerable<SkillCategory>> GetAllActiveSkillCategories()
        {
            var categories = this.context
                              .SkillCategories
                              .Where(s => s.IsDeleted == false)
                              .OrderBy(s => s.Name)
                              .ToList();

            return Task.FromResult(categories.AsEnumerable());
        }

        public Task<SkillCategory> GetCategoryById(string id)
        {
            var category = this.context
                              .SkillCategories
                              .Where(s => s.IsDeleted == false)
                              .SingleOrDefault(s => s.Id == id);

            return Task.FromResult(category);
        }
    }
}

