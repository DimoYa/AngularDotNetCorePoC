using MyEdo.Core.Models;
using MyEdo.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEdo.Web.ApiModels
{
    public class GetSkillsApiModel
    {
        public GetSkillsApiModel(UserSkill userSkill)
        {
            SkillId = userSkill.SkillId;
            SkillName = userSkill.Skill.Name;
            SkillLevel = userSkill.Level;
            CategoryId = userSkill.Skill.SkillCategoryId;
            CategoryName = userSkill.Skill.SkillCategory.Name;
        }

        public string SkillId { get; set; }

        public string SkillName { get; set; }

        public SkillLevel SkillLevel { get; set; }

        public string CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}
