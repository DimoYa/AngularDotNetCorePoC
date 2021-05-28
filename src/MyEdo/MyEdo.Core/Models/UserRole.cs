using Microsoft.AspNetCore.Identity;
using MyEdo.Core.Models.BaseModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace MyEdo.Core.Models
{
    public class UserRole : IdentityRole<string>, IAuditInfo, IDeletableEntity
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
