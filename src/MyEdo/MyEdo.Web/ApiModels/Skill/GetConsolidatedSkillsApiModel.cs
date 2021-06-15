using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyEdo.Web.ApiModels
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
