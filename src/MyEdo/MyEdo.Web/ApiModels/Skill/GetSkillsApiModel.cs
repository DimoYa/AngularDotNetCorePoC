using MyEdo.Core.Models;
using MyEdo.Core.Models.Enums;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

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
            UserId = userSkill.User.Id;
            UserName = userSkill.User.UserName;
        }

        public string SkillId { get; set; }

        public string SkillName { get; set; }

        public SkillLevel SkillLevel { get; set; }

        [IgnoreDataMember]
        [JsonIgnore]
        public string CategoryId { get; set; }

        [IgnoreDataMember]
        [JsonIgnore]
        public string CategoryName { get; set; }

        [IgnoreDataMember]
        [JsonIgnore]
        public string UserId { get; set; }

        [IgnoreDataMember]
        [JsonIgnore]
        public string UserName { get; set; }
    }
}
