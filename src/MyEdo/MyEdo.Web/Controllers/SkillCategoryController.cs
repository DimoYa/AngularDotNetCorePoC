using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyEdo.Business.Services.AppSkillCategory;
using MyEdo.Core.Common;
using MyEdo.Core.Models;
using MyEdo.Web.ApiModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyEdo.Controllers
{
    [Authorize]
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
        [HttpGet(nameof(GetAllCategories))]
        [ProducesResponseType(typeof(SkillCategoryApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SkillCategoryApiModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<SkillCategory>>> GetAllCategories()
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            try
            {
                var skillCategories = await this.skillCategoryService
               .GetAllActiveSkillCategories();

                return Ok(skillCategories);
            }
            catch (Exception ex)
            {

                return this.BadRequest(ex.Message);
            }
        }

        [HttpPost(nameof(CreateCategory))]
        [ProducesResponseType(typeof(SkillCategoryApiModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(SkillCategoryApiModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SkillCategoryApiModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<SkillCategoryApiModel>> CreateCategory([FromBody] SkillCategoryApiModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var skillCategoryId = string.Empty;

            try
            {
                var model = mapper.Map<SkillCategory>(inputModel);
                skillCategoryId = await this.skillCategoryService.CreateCategory(model);
                inputModel.Id = skillCategoryId;
            }
            catch (Exception ex)
            {

                return this.BadRequest(ex.Message);
            }

            return this.CreatedAtAction(nameof(this.CreateCategory), new { skillCategoryId },  inputModel);
        }

        [HttpPut(nameof(EditCategory))]
        [ProducesResponseType(typeof(SkillCategoryApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SkillCategoryApiModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SkillCategoryApiModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<SkillCategoryApiModel>> EditCategory([FromBody] SkillCategoryApiModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            try
            {
                var model = mapper.Map<SkillCategory>(inputModel);
                await this.skillCategoryService.EditCategory(model);
            }
            catch (Exception ex)
            {

                return this.BadRequest(ex.Message);
            }

            return this.Ok(inputModel);
        }

        [HttpDelete(nameof(DeleteCategory))]
        [ProducesResponseType(typeof(SkillCategoryApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SkillCategoryApiModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SkillCategoryApiModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<SkillCategoryApiModel>> DeleteCategory([FromBody] SkillCategoryApiModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            try
            {
                var model = mapper.Map<SkillCategory>(inputModel);
                await this.skillCategoryService.DeleteCategory(model);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }

            return this.Ok(inputModel);
        }
    }
}
