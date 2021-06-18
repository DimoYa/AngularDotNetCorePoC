using MyEdo.Core.Models.Enums;

namespace MyEdo.Web.ApiModels
{
    public class SkillProfileApiModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public SkillLevel Level { get; set; }
    }
}
