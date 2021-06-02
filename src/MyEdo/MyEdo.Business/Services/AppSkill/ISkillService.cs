using MyEdo.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyEdo.Business.Services.AppSkill
{
    public interface ISkillService
    {
        Task<IEnumerable<UserSkill>> GetAllSkillsByCategories();

        Task<IEnumerable<UserSkill>> GetUserSkillsByCategories();

        Task<string> CreateSkill(Skill model);

        Task<bool> EditSkill(Skill model, string id);

        Task<bool> DeleteSkill(string id);

        Task<bool> AddSkillToMyProfile(string id, int level);

        Task<bool> RemoveSkillFromProfile(string id);

        Task<bool> EditSkillLevel(UserSkill model, string id);

        Task<Skill> GetSkillById(string id);

        Task<UserSkill> GetCurrentuserSkillById(string skillId);

        Task<IList<string>> GetCurrentUserSkillsId();
    }
}
