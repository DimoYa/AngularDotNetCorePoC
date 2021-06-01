using MyEdo.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyEdo.Business.Services.AppSkillCategory
{
    public interface ISkillCategoryService
    {
        Task<string> CreateCategory(SkillCategory model);

        Task<bool> EditCategory(SkillCategory model);

        Task<bool> DeleteCategory(SkillCategory model);

        Task<SkillCategory> GetCategoryByName(string name);

        Task<SkillCategory> GetCategoryById(string id);

        Task<IEnumerable<SkillCategory>> GetAllActiveSkillCategories();
    }
}
