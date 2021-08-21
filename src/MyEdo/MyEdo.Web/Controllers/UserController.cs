namespace MyEdo.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using MyEdo.Business.Exceptions;
    using MyEdo.Business.Services.AppUser;
    using MyEdo.Core.Common;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," +
                           GlobalConstants.ResourceRoleName)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet(nameof(GetUserRoles))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<List<string>>> GetUserRoles()
        {
            try
            {
                var userRoles = await this.userService
               .GetUserRoles();

                return Ok(userRoles);
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
    }
}
