using MyEdo.Core.Models.Enums;

namespace MyEdo.Web.ApiModels.Trainings
{
    public class TrainingApiModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public TrainingType Type { get; set; }

        public TrainingStatus Status { get; set; }
    }
}
