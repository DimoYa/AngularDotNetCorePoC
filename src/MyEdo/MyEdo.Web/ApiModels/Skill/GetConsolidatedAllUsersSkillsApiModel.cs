using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEdo.Web.ApiModels
{
    public class GetConsolidatedAllUsersSkillsApiModel
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public IEnumerable<GetConsolidatedSkillsApiModel> Categories { get; set; }
    }
}
