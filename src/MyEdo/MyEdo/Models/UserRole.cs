using Microsoft.AspNetCore.Identity;
using MyEdo.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyEdo.Models
{
    public class UserRole : IdentityRole, IAuditInfo, IDeletableEntity
    {
        public UserRole(string name)
            : base(name)
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
