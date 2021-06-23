using System;
using System.ComponentModel.DataAnnotations;

namespace MyEdo.Core.Models.BaseModels
{
    public abstract class BaseModel<TKey> : IAuditInfo
    {
        public BaseModel()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.ModifiedOn = DateTime.UtcNow;
        }

        [Key]
        public TKey Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
