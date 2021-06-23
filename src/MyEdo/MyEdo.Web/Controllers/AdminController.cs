using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyEdo.Business.Exceptions;
using MyEdo.Business.Services.AppAdmin;
using MyEdo.Business.Services.AppUser;
using MyEdo.Core.Common;
using MyEdo.Web.ApiModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEdo.Controllers
{
    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService adminService;
        private readonly IUserService userService;

        public AdminController(
            IAdminService adminService,
            IUserService userService)
        {
            this.adminService = adminService;
            this.userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<GetAllUsersApiModel>>> GetAllSkillsByCategories()
        {
            try
            {
                var users = await this.adminService
               .GetAllActiveUsers();

                var model = users.Select(s => new GetAllUsersApiModel(s)).ToList();
                model.ForEach(x => x.Roles.ToList().ForEach(async r => r.Name = await this.userService.GetRoleNameById(r.Id)));

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

        [HttpPut("[action]/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<string>> LockUser(string userId)
        {
            try
            {
                await this.adminService.Lock(userId);
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

            return this.Ok(userId);
        }

        [HttpPut("[action]/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> UnLockUser([FromBody] string userId)
        {
            try
            {
                await this.adminService.Unlock(userId);
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

            return this.Ok(userId);
        }
    }
}
