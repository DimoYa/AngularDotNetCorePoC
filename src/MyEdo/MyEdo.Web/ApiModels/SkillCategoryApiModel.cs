using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyEdo.Web.ApiModels
{
    public class SkillCategoryApiModel
    {
        [Required]
        [StringLength(20, MinimumLength = 1)]
        [Display(Name = "Skill category name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
    }
}
