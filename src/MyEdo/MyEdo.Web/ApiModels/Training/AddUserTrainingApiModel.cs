using MyEdo.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEdo.Web.ApiModels.Training
{
    public class AddUserTrainingApiModel
    {
        public string TrainingId { get; set; }

        public string UserId { get; set; }

        public UserTrainingStatus Status { get; set; }
    }
}
