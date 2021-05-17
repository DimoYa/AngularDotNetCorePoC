using MyEdo.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyEdo.Models
{
    public class Skill : BaseDeletableModel<string>
    {
        public Skill()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Users = new HashSet<UserSkill>();
        }

        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        [ForeignKey(nameof(SkillCategory))]
        public string SkillCategoryId { get; set; }

        public virtual SkillCategory SkillCategory { get; set; }

        public virtual ICollection<UserSkill> Users { get; set; }
    }
}