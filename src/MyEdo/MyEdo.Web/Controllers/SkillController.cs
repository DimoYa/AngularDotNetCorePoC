using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyEdo.Business.Exceptions;
using MyEdo.Business.Services.AppSkill;
using MyEdo.Core.Common;
using MyEdo.Core.Models;
using MyEdo.Web.ApiModels.Skills;
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
        [HttpGet(nameof(GetAllSkillsByCategories))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<GetConsolidatedSkillsByCategoryApiModel>>> GetAllSkillsByCategories()
        {
            try
            {
                var skills = await this.skillService
               .GetAllSkillsByCategories();

                var model = skills.Select(s => new GetSkillsByCategoriesApiModel(s)).ToList();

                var groupedUserSkillInfo = model
                .GroupBy(s => new
                {
                    s.CategoryId,
                    s.CategoryName
                })
               .Select(grp => new GetConsolidatedSkillsByCategoryApiModel
               {
                   CategoryId = grp.Key.CategoryId,
                   CategoryName = grp.Key.CategoryName,
                   Skills = grp.ToList(),
               })
               .ToList();

                return Ok(groupedUserSkillInfo);
            }
            catch (NotAuthorizedException ex)
            {
                return this.Unauthorized(ex.Message);
            }
            catch (ForbiddenException ex)
            {
                return this.Forbid(ex.Message);
            }
            catch (BadRequestException ex)
            {
                return this.BadRequest(ex.Message);
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
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<GetConsolidatedAllUsersSkillsApiModel>>> GetAllUsersSkills()
        {
            try
            {
                var skills = await this.skillService
               .GetAllUsersSkills();

                var model = skills.Select(s => new GetSkillsApiModel(s)).ToList();

                var groupedUserSkillInfo = model
                .GroupBy(s => new
                {
                    s.CategoryId,
                    s.CategoryName,
                    s.UserId,
                    s.UserName
                })
               .Select(grp => new GetConsolidatedSkillsApiModel
               {
                   CategoryId = grp.Key.CategoryId,
                   CategoryName = grp.Key.CategoryName,
                   UserId = grp.Key.UserId,
                   UserName = grp.Key.UserName,
                   Skills = grp.ToList(),
               }).GroupBy(s => new
               {
                   s.UserId,
                   s.UserName
               })
               .Select(grp => new GetConsolidatedAllUsersSkillsApiModel
               {
                   UserId = grp.Key.UserId,
                   UserName = grp.Key.UserName,
                   Categories = grp.ToList(),
               })
               .ToList();

                return Ok(groupedUserSkillInfo);
            }
            catch (NotAuthorizedException ex)
            {
                return this.Unauthorized(ex.Message);
            }
            catch (ForbiddenException ex)
            {
                return this.Forbid(ex.Message);
            }
            catch (BadRequestException ex)
            {
                return this.BadRequest(ex.Message);
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
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<GetConsolidatedSkillsApiModel>>> GetMySkills()
        {
            try
            {
                var skills = await this.skillService
               .GetMySkills();

                var model = skills.Select(s => new GetSkillsApiModel(s));

                var groupedUserSkillInfo = model
                .GroupBy(s => new
                {
                    s.CategoryId,
                    s.CategoryName
                })
               .Select(grp => new GetConsolidatedSkillsApiModel
               {
                   CategoryId = grp.Key.CategoryId,
                   CategoryName = grp.Key.CategoryName,
                   Skills = grp.ToList(),
               })
               .ToList();

                return Ok(groupedUserSkillInfo);
            }
            catch (NotAuthorizedException ex)
            {
                return this.Unauthorized(ex.Message);
            }
            catch (ForbiddenException ex)
            {
                return this.Forbid(ex.Message);
            }
            catch (BadRequestException ex)
            {
                return this.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost]
        [ProducesResponseType(typeof(SkillApiModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateSkill(SkillApiModel model)
        {
            var skillId = string.Empty;

            try
            {
                var modelMap = mapper.Map<Skill>(model);
                skillId = await this.skillService.CreateSkill(modelMap);
                model.Id = skillId;
            }
            catch (NotAuthorizedException ex)
            {
                return this.Unauthorized(ex.Message);
            }
            catch (ForbiddenException ex)
            {
                return this.Forbid(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return this.Unauthorized(ex.GenerateApiError());
            }
            catch (BadRequestException ex)
            {
                return this.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {

                return this.BadRequest(ex.Message);
            }

            return this.CreatedAtAction(nameof(this.CreateSkill), new { skillId }, model);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPut]
        [ProducesResponseType(typeof(SkillApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SkillApiModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SkillApiModel), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(SkillApiModel), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EditSkill(SkillApiModel model)
        {
            try
            {
                var modelMap = mapper.Map<Skill>(model);
                await this.skillService.EditSkill(modelMap);
            }
            catch (NotAuthorizedException ex)
            {
                return this.Unauthorized(ex.Message);
            }
            catch (ForbiddenException ex)
            {
                return this.Forbid(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return this.Unauthorized(ex.GenerateApiError());
            }
            catch (BadRequestException ex)
            {
                return this.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {

                return this.BadRequest(ex.Message);
            }

            return this.Ok(model);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SkillApiModel>> DeleteSkill([FromBody] SkillDeleteApiModel model)
        {
            try
            {
                await this.skillService.DeleteSkill(model.Id);
            }
            catch (NotAuthorizedException ex)
            {
                return this.Unauthorized(ex.Message);
            }
            catch (ForbiddenException ex)
            {
                return this.Forbid(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return this.Unauthorized(ex.GenerateApiError());
            }
            catch (BadRequestException ex)
            {
                return this.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }

            return this.Ok(model);
        }

        [Authorize(Roles = GlobalConstants.ResourceRoleName)]
        [HttpPut(nameof(AddSkillToMyProfile))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddSkillToMyProfile([FromBody] SkillProfileApiModel model)
        {
            try
            {
                var modelMap = mapper.Map<UserSkill>(model);
                await this.skillService.AddSkillToMyProfile(modelMap);
            }
            catch (NotAuthorizedException ex)
            {
                return this.Unauthorized(ex.Message);
            }
            catch (ForbiddenException ex)
            {
                return this.Forbid(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return this.Unauthorized(ex.GenerateApiError());
            }
            catch (BadRequestException ex)
            {
                return this.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }

            return this.Ok(model);
        }

        [Authorize(Roles = GlobalConstants.ResourceRoleName)]
        [HttpPut(nameof(EditSkillLevel))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EditSkillLevel([FromBody] SkillProfileApiModel model)
        {
            try
            {
                var modelMap = mapper.Map<UserSkill>(model);
                await this.skillService.EditSkillLevel(modelMap);
            }
            catch (NotAuthorizedException ex)
            {
                return this.Unauthorized(ex.Message);
            }
            catch (ForbiddenException ex)
            {
                return this.Forbid(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return this.Unauthorized(ex.GenerateApiError());
            }
            catch (BadRequestException ex)
            {
                return this.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }

            return this.Ok(model);
        }

        [Authorize(Roles = GlobalConstants.ResourceRoleName)]
        [HttpDelete(nameof(RemoveSkillFromMyProfile))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveSkillFromMyProfile([FromBody] SkillDeleteApiModel model)
        {
            try
            {
                await this.skillService.RemoveSkillFromProfile(model.Id);
            }
            catch (NotAuthorizedException ex)
            {
                return this.Unauthorized(ex.Message);
            }
            catch (ForbiddenException ex)
            {
                return this.Forbid(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return this.Unauthorized(ex.GenerateApiError());
            }
            catch (BadRequestException ex)
            {
                return this.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }

            return this.Ok(model);
        }
    }
}
