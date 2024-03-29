﻿using System.Collections.Generic;

namespace MyEdo.Web.ApiModels.Skills
{
    public class GetConsolidatedAllUsersSkillsApiModel
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public IEnumerable<GetConsolidatedSkillsApiModel> Categories { get; set; }
    }
}
