using MyEdo.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyEdo.Models
{
    public class SkillCategory : BaseDeletableModel<string>
    {
        public SkillCategory()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
            this.Skills = new HashSet<Skill>();
        }

        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string Name { get; set; }

        public virtual ICollection<Skill> Skills { get; set; }
    }
}