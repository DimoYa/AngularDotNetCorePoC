using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace MyEdo.Web.ApiModels.Skills
{
    public class GetConsolidatedSkillsApiModel
    {
        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        [IgnoreDataMember]
        [JsonIgnore]
        public string UserId { get; set; }

        [IgnoreDataMember]
        [JsonIgnore]
        public string UserName { get; set; }

        public IEnumerable<GetSkillsApiModel> Skills { get; set; }
    }
}
