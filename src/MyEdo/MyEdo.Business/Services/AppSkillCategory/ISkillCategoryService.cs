using MyEdo.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyEdo.Business.Services.AppSkillCategory
{
    public interface ISkillCategoryService
    {
        Task<bool> CreateCategory(SkillCategory model);

        Task<bool> EditCategory(SkillCategory model, string id);

        Task<bool> DeleteCategory(string id);

        Task<SkillCategory> GetCategoryByName(string name);

        Task<SkillCategory> GetCategoryById(string id);

        Task<IEnumerable<SkillCategory>> GetAllActiveSkillCategories();
    }
}
