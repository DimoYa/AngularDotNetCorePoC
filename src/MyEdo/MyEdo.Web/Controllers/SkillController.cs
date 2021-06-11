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
    [Authorize]
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

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet(nameof(GetAllSkills))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<SkillCategory>>> GetAllSkills()
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            try
            {
                var skills = await this.skillService
               .GetAllSkillsByCategories();

                return Ok(skills);
            }
            catch (Exception ex)
            {

                return this.BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet(nameof(GetAllUsersSkills))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<UserSkill>>> GetAllUsersSkills()
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            try
            {
                var skills = await this.skillService
               .GetAllUsersSkills();

                return Ok(skills);
            }
            catch (Exception ex)
            {

                return this.BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = GlobalConstants.ResourceRoleName)]
        [HttpGet(nameof(GetMySkills))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<GetSkillsApiModel>>> GetMySkills()
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            try
            {
                var skills = await this.skillService
               .GetMySkills();

                return Ok(skills.Select(s=> new GetSkillsApiModel(s)));
            }
            catch (Exception ex)
            {

                return this.BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost(nameof(CreateSkill))]
        [ProducesResponseType(typeof(SkillApiModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPut(nameof(EditSkill))]
        [ProducesResponseType(typeof(SkillApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SkillApiModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SkillApiModel), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> EditSkill(SkillApiModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            try
            {
                var model = mapper.Map<Skill>(inputModel);
                await this.skillService.EditSkill(model);
            }
            catch (Exception ex)
            {

                return this.BadRequest(ex.Message);
            }

            return this.Ok(inputModel);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpDelete(nameof(DeleteSkill))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<SkillApiModel>> DeleteSkill([FromBody] SkillDeleteApiModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            try
            {
                await this.skillService.DeleteSkill(inputModel.SkillId);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }

            return this.Ok(inputModel);
        }

        [Authorize(Roles = GlobalConstants.ResourceRoleName)]
        [HttpPut(nameof(AddSkillToMyProfile))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddSkillToMyProfile([FromBody] SkillAddApiModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            try
            {
                var model = mapper.Map<UserSkill>(inputModel);
                await this.skillService.AddSkillToMyProfile(model);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }

            return this.Ok(inputModel);
        }

        [Authorize(Roles = GlobalConstants.ResourceRoleName)]
        [HttpPut(nameof(EditSkillLevel))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> EditSkillLevel([FromBody] SkillAddApiModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            try
            {
                var model = mapper.Map<UserSkill>(inputModel);
                await this.skillService.EditSkillLevel(model);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }

            return this.Ok(inputModel);
        }

        [Authorize(Roles = GlobalConstants.ResourceRoleName)]
        [HttpDelete(nameof(RemoveSkillFromMyProfile))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RemoveSkillFromMyProfile([FromBody] SkillDeleteApiModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            try
            {
                await this.skillService.RemoveSkillFromProfile(inputModel.SkillId);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }

            return this.Ok(inputModel);
        }
    }
}
