using MyEdo.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyEdo.Business.Services.AppSkill
{
    public interface ISkillService
    {
        Task<IEnumerable<Skill>> GetAllSkillsByCategories();

        Task<IEnumerable<UserSkill>> GetAllUsersSkills();

        Task<IEnumerable<UserSkill>> GetMySkills();

        Task<string> CreateSkill(Skill model);

        Task<bool> EditSkill(Skill model);

        Task<bool> DeleteSkill(string skillId);

        Task<bool> AddSkillToMyProfile(UserSkill model);

        Task<bool> RemoveSkillFromProfile(string id);

        Task<bool> EditSkillLevel(UserSkill model);
    }
}
