using MyEdo.Core.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyEdo.Core.Models
{
    public class UserTraining
    {
        public string UserId { get; set; }
        public virtual User User { get; set; }

        public string TrainingId { get; set; }
        public virtual Training Training { get; set; }

        [Required]
        public UserTrainingStatus Status { get; set; }
    }
}