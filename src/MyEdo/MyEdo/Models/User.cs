using Microsoft.AspNetCore.Identity;
using MyEdo.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyEdo.Models
{
    public class User : IdentityUser<string>, IAuditInfo, IDeletableEntity
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Trainings = new HashSet<UserTraining>();
            this.Skills = new HashSet<UserSkill>();
        }

        [Required]
        [RegularExpression("[A-Z][a-z]+", ErrorMessage = "{0} should contain only letters starting with a capital case")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression("[A-Z][a-z]+", ErrorMessage = "{0} should contain only letters starting with a capital case")]
        public string LastName { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<UserTraining> Trainings { get; set; }

        public virtual ICollection<UserSkill> Skills { get; set; }
    }
}
