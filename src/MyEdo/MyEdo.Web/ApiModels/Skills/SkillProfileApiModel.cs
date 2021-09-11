using MyEdo.Core.Models.Enums;

namespace MyEdo.Web.ApiModels.Skills
{
    public class SkillProfileApiModel
    {
        public string SkillId { get; set; }

        public string SkillName { get; set; }

        public SkillLevel Level { get; set; }
    }
}
