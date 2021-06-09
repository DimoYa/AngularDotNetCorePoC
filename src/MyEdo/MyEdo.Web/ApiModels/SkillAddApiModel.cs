using MyEdo.Core.Models;
using MyEdo.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEdo.Web.ApiModels
{
    public class SkillAddApiModel
    {
        public SkillLevel SkillLevel { get; set; }

        public SkillApiModel Skill { get; set; }
    }
}
