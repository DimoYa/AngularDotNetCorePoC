using MyEdo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyEdo.Web.ApiModels
{
    public class GetSkillsByCategoriesApiModel
    {
        public GetSkillsByCategoriesApiModel(Skill skill)
        {
            this.Skillid = skill.Id;
            this.SkillName = skill.Name;
            this.CategoryId = skill.SkillCategoryId;
            this.CategoryName = skill.SkillCategory.Name;
        }
        public string Skillid { get; set; }

        public string SkillName { get; set; }

        [IgnoreDataMember]
        [JsonIgnore]
        public string CategoryId { get; set; }

        [IgnoreDataMember]
        [JsonIgnore]
        public string CategoryName { get; set; }
    }
}
