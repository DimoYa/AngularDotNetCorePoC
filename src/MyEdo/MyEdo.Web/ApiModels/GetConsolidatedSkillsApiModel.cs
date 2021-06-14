using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEdo.Web.ApiModels
{
    public class GetConsolidatedSkillsApiModel
    {
        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public IEnumerable<GetSkillsApiModel> Skills { get; set; }
    }
}
