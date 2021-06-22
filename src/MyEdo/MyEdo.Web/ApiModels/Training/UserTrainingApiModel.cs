using MyEdo.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyEdo.Web.ApiModels.Training
{
    public class UserTrainingApiModel
    {
        public string TrainingId { get; set; }

        public string TrainingName { get; set; }

        [IgnoreDataMember]
        [JsonIgnore]
        public string UserId { get; set; }

        [IgnoreDataMember]
        [JsonIgnore]
        public string UserName { get; set; }

        public UserTrainingStatus Status { get; set; }
    }
}
