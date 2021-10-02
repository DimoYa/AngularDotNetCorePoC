namespace MyEdo.Web.ApiModels.Trainings
{
    using MyEdo.Core.Models.Enums;
    using System;

    public class TrainingApiModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public TrainingType Type { get; set; }

        public TrainingStatus Status { get; set; }

        public DateTime DueDate { get; set; }
    }
}
