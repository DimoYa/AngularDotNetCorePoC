using System;

namespace MyEdo.Core.Models.BaseModels
{
    interface IDeletableEntity
    {
        bool IsDeleted { get; set; }

        DateTime? DeletedOn { get; set; }
    }
}
