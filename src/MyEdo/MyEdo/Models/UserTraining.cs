using MyEdo.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyEdo.Models
{
    public class UserTraining
    {
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey(nameof(Training))]
        public string TrainingId { get; set; }
        public virtual Training Training { get; set; }

        [Required]
        public UserTrainingStatus Status { get; set; }
    }
}