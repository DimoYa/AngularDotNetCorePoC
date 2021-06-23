using System.Collections.Generic;

namespace MyEdo.Web.ApiModels.Trainings
{
    public class GetConsolidatedUserTrainingsApiModel
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public IEnumerable<UserTrainingApiModel> Trainings { get; set; }
    }
}
