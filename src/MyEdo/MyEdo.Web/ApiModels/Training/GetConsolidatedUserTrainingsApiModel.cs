using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEdo.Web.ApiModels.Training
{
    public class GetConsolidatedUserTrainingsApiModel
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public IEnumerable<UserTrainingApiModel> Trainings { get; set; }
    }
}
