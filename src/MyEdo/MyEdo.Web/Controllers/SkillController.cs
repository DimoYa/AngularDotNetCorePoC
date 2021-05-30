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
    [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," +
                          GlobalConstants.ResourceRoleName)]
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly ISkillCategoryService skillCategoryService;
        private readonly IMapper mapper;

        public SkillController(
            ISkillCategoryService skillCategoryService,
            IMapper mapper)
        {
            this.skillCategoryService = skillCategoryService;
            this.mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(SkillCategoryApiModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(SkillCategoryApiModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SkillCategoryApiModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<SkillCategoryApiModel>> CreateCategory([FromBody] SkillCategoryApiModel inputModel)
        {
            if (string.IsNullOrEmpty(this.User.Identity.Name))
            {
                return this.Unauthorized();
            }
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            try
            {
                var model = mapper.Map<SkillCategory>(inputModel);
                await this.skillCategoryService.CreateCategory(model);
            }
            catch (Exception ex)
            {

                return this.BadRequest(ex.Message);
            }

            return this.CreatedAtAction(nameof(this.CreateCategory), inputModel);
        }

        [HttpGet]
        [ProducesResponseType(typeof(SkillCategoryApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SkillCategoryApiModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<SkillCategory>>> Get()
        {
            if (string.IsNullOrEmpty(this.User.Identity.Name))
            {
                return this.Unauthorized();
            }
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
    }
}
