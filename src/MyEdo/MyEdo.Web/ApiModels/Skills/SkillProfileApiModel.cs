using MyEdo.Core.Models.Enums;

namespace MyEdo.Web.ApiModels.Skills
{
    public class SkillProfileApiModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public SkillLevel Level { get; set; }
    }
}
