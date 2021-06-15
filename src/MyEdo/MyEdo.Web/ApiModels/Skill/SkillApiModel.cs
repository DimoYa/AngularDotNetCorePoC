using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEdo.Web.ApiModels
{
    public class SkillApiModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string SkillCategoryId { get; set; }

        public string SkillCategoryName { get; set; }
    }
}
