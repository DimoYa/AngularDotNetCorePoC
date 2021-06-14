using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEdo.Web.ApiModels
{
    public class GetConsolidatedSkillsByCategoryApiModel
    {
        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public IEnumerable<GetSkillsByCategoriesApiModel> Skills { get; set; }
    }
}
