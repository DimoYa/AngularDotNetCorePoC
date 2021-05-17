using MyEdo.Models.BaseModels;
using MyEdo.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyEdo.Models
{
    public class Training : BaseDeletableModel<string>
    {
        public Training()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Users = new HashSet<UserTraining>();
        }

        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        public TrainingType Type { get; set; }

        [Required]
        public TrainingStatus Status { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public virtual ICollection<UserTraining> Users { get; set; }
    }
}