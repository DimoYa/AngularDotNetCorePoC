using MyEdo.Core.Models;
using MyEdo.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEdo.Web.ApiModels
{
    public class SkillProfileApiModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public SkillLevel Level { get; set; }
    }
}
