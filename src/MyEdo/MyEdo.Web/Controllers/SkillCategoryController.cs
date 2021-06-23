namespace MyEdo.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using MyEdo.Business.Exceptions;
    using MyEdo.Business.Services.AppSkillCategory;
    using MyEdo.Core.Common;
    using MyEdo.Core.Models;
    using MyEdo.Web.ApiModels.SkillCategories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Route("api/[controller]")]
    [ApiController]
    public class SkillCategoryController : ControllerBase
    {
        private readonly ISkillCategoryService skillCategoryService;
        private readonly IMapper mapper;

        public SkillCategoryController(
            ISkillCategoryService skillCategoryService,
            IMapper mapper)
        {
            this.skillCategoryService = skillCategoryService;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<CategoryApiModel>>> GetAllCategories()
        {
            try
            {
                var skillCategories = await this.skillCategoryService
               .GetAllActiveSkillCategories();

                var model = skillCategories
                    .Select(c => new CategoryApiModel { Id = c.Id, Name = c.Name });

                return Ok(model);
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

        [HttpPost]
        [ProducesResponseType(typeof(CategoryApiModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<CategoryApiModel>> CreateCategory([FromBody] CategoryApiModel model)
        {
            var skillCategoryId = string.Empty;

            try
            {
                var modelMap = mapper.Map<SkillCategory>(model);
                skillCategoryId = await this.skillCategoryService.CreateCategory(modelMap);
                model.Id = skillCategoryId;
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

            return this.CreatedAtAction(nameof(this.CreateCategory), new { skillCategoryId }, model);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<CategoryApiModel>> EditCategory([FromBody] CategoryApiModel model)
        {
            try
            {
                var modelMap = mapper.Map<SkillCategory>(model);
                await this.skillCategoryService.EditCategory(modelMap);
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
                return this.NotFound(ex.GenerateApiError());
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

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<CategoryApiModel>> DeleteCategory([FromBody] CategoryApiModel model)
        {
            try
            {
                await this.skillCategoryService.DeleteCategory(model.Id);
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
                return this.NotFound(ex.GenerateApiError());
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
