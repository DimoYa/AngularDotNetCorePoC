using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyEdo.Business.Services.AppSkill;
using MyEdo.Core.Common;
using MyEdo.Core.Models;
using MyEdo.Web.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEdo.Web.Controllers
{
    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly ISkillService skillService;
        private readonly IMapper mapper;

        public SkillController(
            ISkillService skillService,
            IMapper mapper)
        {
            this.skillService = skillService;
            this.mapper = mapper;
        }

        [HttpPost(nameof(CreateSkill))]
        [ProducesResponseType(typeof(SkillApiModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(SkillApiModel), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateSkill(SkillApiModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var skillId = string.Empty;

            try
            {
                var model = mapper.Map<Skill>(inputModel);
                skillId = await this.skillService.CreateSkill(model);
                inputModel.Id = skillId;
            }
            catch (Exception ex)
            {

                return this.BadRequest(ex.Message);
            }

            return this.CreatedAtAction(nameof(this.CreateSkill), new { skillId }, inputModel);
        }
    }
}
