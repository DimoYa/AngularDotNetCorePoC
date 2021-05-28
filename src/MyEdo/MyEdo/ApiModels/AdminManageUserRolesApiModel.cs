using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEdo.ApiModels
{
    public class AdminManageUserRolesApiModel
    {
        public string FullName { get; set; }
        public bool Resource { get; set; }
        public bool Admin { get; set; }
    }
}
