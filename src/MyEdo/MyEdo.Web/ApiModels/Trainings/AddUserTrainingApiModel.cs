using MyEdo.Core.Models.Enums;

namespace MyEdo.Web.ApiModels.Trainings
{
    public class AddUserTrainingApiModel
    {
        public string TrainingId { get; set; }

        public string UserId { get; set; }

        public UserTrainingStatus Status { get; set; }
    }
}
