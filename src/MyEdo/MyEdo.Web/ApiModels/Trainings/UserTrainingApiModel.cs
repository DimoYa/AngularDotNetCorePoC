using MyEdo.Core.Models.Enums;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace MyEdo.Web.ApiModels.Trainings
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
