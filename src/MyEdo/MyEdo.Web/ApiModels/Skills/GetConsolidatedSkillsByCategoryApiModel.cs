using System.Collections.Generic;

namespace MyEdo.Web.ApiModels.Skills
{
    public class GetConsolidatedSkillsByCategoryApiModel
    {
        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public IEnumerable<GetSkillsByCategoriesApiModel> Skills { get; set; }
    }
}
