using MyEdo.Core.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyEdo.Core.Models
{
    public class UserSkill
    {
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey(nameof(Skill))]
        public string SkillId { get; set; }
        public virtual Skill Skill { get; set; }

        [Required]
        public SkillLevel Level { get; set; }
    }
}